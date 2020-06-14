using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Golem.ActivityApi.Client.Swagger.Api;
using Golem.Provider.Entities;
using Golem.Provider.Repository;

namespace Golem.Provider.ActivityControl
{
    public class ActivityController
    {
        /// <summary>
        /// The Activity API proxy that this controller will listen to and handle.
        /// </summary>
        IProviderGatewayApi ActivityApi { get; set; }

        /// <summary>
        /// ExeUnit factory.
        /// </summary>
        IExeUnitFactory ExeUnitFactory { get; set; }

        public IAgreementRepository AgreementRepository { get; set; }

        public IActivityRepository ActivityRepository { get; set; }


        /// <summary>
        /// Dictionary of ExeUnit instances indexed by ActivityId
        /// </summary>
        public IDictionary<string, IExeUnit> ExeUnitsByActivityId { get; set; } = new Dictionary<string, IExeUnit>();

        public ActivityController(
            IProviderGatewayApi activityApi,
            IExeUnitFactory exeUnitFactory,
            IAgreementRepository agreementRepository,
            IActivityRepository activityRepository)
        {
            this.ActivityApi = activityApi;
            this.ExeUnitFactory = exeUnitFactory;
            this.AgreementRepository = agreementRepository;
            this.ActivityRepository = activityRepository;
        }

        private bool isStopped = false;

        /// <summary>
        /// Start the ActivityController to listen to ActivityApi endpoint and handle the traffic accordingly.
        /// </summary>
        public void Run()
        {
            isStopped = false;

            while (!isStopped)
            {
                // Collect the Activity API events

                var events = this.ActivityApi.CollectActivityEvents(5000);

                // Handle the CreateActivity calls

                foreach(var evn in events)
                {
                    switch(evn.EventType)
                    {
                        case "CreateActivity":
                            this.CreateActivity(evn as ActivityApi.Client.Swagger.Model.CreateActivityProviderEvent);
                            break;
                        case "Exec":
                            this.Exec(evn as ActivityApi.Client.Swagger.Model.ExecProviderEvent);
                            break;
                        case "DestroyActivity":
                            this.DestroyActivity(evn);
                            break;
                        case "GetState":
                            this.GetStateDetails(evn);
                            break;
                        default:
                            // TODO log unknown event type
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Stop the ActivityController
        /// </summary>
        public void Stop()
        {
            isStopped = true;
        }

        /// <summary>
        /// The handler method to handle the incoming Create Activity requests.
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <returns></returns>
        protected Activity CreateActivity(ActivityApi.Client.Swagger.Model.CreateActivityProviderEvent eventDetails)
        {
            Agreement agreement = null;

            // We assume that the client has validated the existence of Agreement, so we assume Agreement is there
            if((agreement = this.AgreementRepository.GetAgreement(eventDetails.AgreementId)) != null)
            {
                // ...should we validate the Agreement again here???
                // 
                // ...should we consult ResourceManager to pre-reserve the resources???
                               
                // All is confirmed, create Activity entity, build relevant ExeUnit

                var exeUnit = this.ExeUnitFactory.BuildExeUnit(agreement);

                var activity = new Activity()
                {
                    Agreement = agreement,
                    Id = eventDetails.ActivityId,
                    ExeUnitInstance = exeUnit
                };

                exeUnit.OnStateChanged += ExeUnit_OnStateChanged;
                
                activity.SetState(ActivityState.New);

                activity = this.ActivityRepository.CreateActivity(activity);


                var createActivityResult = exeUnit.CreateActivity(activity);

                // in case of ExeUnit error - set activity immediately to Terminated state
                if(createActivityResult.Result == ExeUnitResultType.Error)
                {
                    activity.SetState(ActivityState.Terminated);
                    this.ActivityRepository.SetActivityState(activity.Id, ActivityState.Terminated);
                }
                
                // store the ExeUnit in dictionary
                this.ExeUnitsByActivityId.Add(activity.Id, exeUnit);

                return activity;

            }
            else
            {
                throw new Exception($"Agreement Id {eventDetails.AgreementId} not found!");
            }
        }

        /// <summary>
        /// Handler to process incoming Exec events for activity.
        /// Spawns a sequential command processor in separate thread.
        /// </summary>
        /// <param name="eventDetails"></param>
        protected void Exec(ActivityApi.Client.Swagger.Model.ExecProviderEvent eventDetails)
        {
            Task.Run(() => { this.DoExec(eventDetails); }); 
        }

        protected void DoExec(ActivityApi.Client.Swagger.Model.ExecProviderEvent eventDetails)
        {
            if (!this.ExeUnitsByActivityId.ContainsKey(eventDetails.ActivityId))
                throw new Exception($"ExeUnit for Activity {eventDetails.ActivityId} not found!");

            var exeUnit = this.ExeUnitsByActivityId[eventDetails.ActivityId];

            int i = 0;

            foreach(var command in eventDetails.ExeScript.Commands)
            {
                var entCommand = new ExeScriptCommand()
                {
                    Command = command.Command,
                    Params = command.Params
                };

                var entResult = exeUnit.ExecCommand(entCommand);
                entResult.Message = $"activity state is {exeUnit.Activity.State} after {entCommand}.";
                bool isBatchFinished = false;

                // set IsBatchFinished if we have processed the last command in batch or error
                if (entResult.Result == ExeUnitResultType.Error || i == (eventDetails.ExeScript.Commands.Count - 1))
                {
                    isBatchFinished = true;
                }

                var commandResult = new ActivityApi.Client.Swagger.Model.ExeScriptCommandResult()
                {
                    Index = i,
                    IsBatchFinished = isBatchFinished,  
                    Message = entResult.Message,
                    Result = entResult.Result.ToString()
                };

                this.ActivityApi.PutExeScriptResult(eventDetails.ActivityId, eventDetails.BatchId, commandResult);

                i++;
            }

        }

        /// <summary>
        /// Handler for incoming DestroyActivity events.
        /// </summary>
        /// <param name="eventDetails"></param>
        protected void DestroyActivity(ActivityApi.Client.Swagger.Model.ProviderEvent eventDetails)
        {
            if (!this.ExeUnitsByActivityId.ContainsKey(eventDetails.ActivityId))
                throw new Exception($"ExeUnit for Activity {eventDetails.ActivityId} not found!");

            var exeUnit = this.ExeUnitsByActivityId[eventDetails.ActivityId];

            exeUnit.Destroy();

        }

        protected void GetStateDetails(ActivityApi.Client.Swagger.Model.ProviderEvent eventDetails)
        {
            if (!this.ExeUnitsByActivityId.ContainsKey(eventDetails.ActivityId))
                throw new Exception($"ExeUnit for Activity {eventDetails.ActivityId} not found!");

            var exeUnit = this.ExeUnitsByActivityId[eventDetails.ActivityId];

            var stateDetails = exeUnit.GetStateDetails();

            this.ActivityApi.PutActivityStateDetails(eventDetails.ActivityId, new Golem.ActivityApi.Client.Swagger.Model.ActivityStateDetails()
            {
                State = $"{stateDetails.State}",
                CurrentUsage = stateDetails.CurrentUsage.Select(item => (double?)item).ToList()
            });

        }


        private void ExeUnit_OnStateChanged(ExeUnitStateDetails newStateDetails)
        {
            if (newStateDetails.ActivityId == null)
                throw new Exception("Activity Id not initialized in ExeUnitStateDetails!");

            // update Activity state in repo

            this.ActivityRepository.SetActivityState(newStateDetails.ActivityId, (ActivityState)Enum.Parse(typeof(ActivityState), $"{newStateDetails.State}"));

            // now send the state update to Provider gateway

            this.ActivityApi.PutActivityStateDetails(newStateDetails.ActivityId, new ActivityApi.Client.Swagger.Model.ActivityStateDetails()
            {
                State = $"{newStateDetails.State}",
                ErrorMessage = String.Empty,
                CurrentUsage = null  // do not send usage (as we don't know it)
            });
        }
    }
}
