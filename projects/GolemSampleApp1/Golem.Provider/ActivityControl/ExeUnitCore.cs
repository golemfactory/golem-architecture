using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.ActivityControl
{
    public class ExeUnitCore<IWorker> : IExeUnit
        where IWorker : IExeUnitWorker
    {
        public Activity Activity { get; set; }
        protected IExeUnitWorker Worker { get; set; }

        public event StateChanged OnStateChanged;

        public CreateActivityResult CreateActivity(Activity activity)
        {
            this.Activity = activity;

            return Worker.CreateActivity(activity);
        }

        public void Destroy()
        {
            Worker.DestroyActivity();
        }

        public ExeScriptCommandResult ExecCommand(ExeScriptCommand command)
        {
            switch(this.ParseCommand(command.Command))
            {
                case ExeUnitCommand.Deploy:
                    return this.Worker.Deploy(command, 0);
                case ExeUnitCommand.Start:
                    return this.Worker.Start(command, 0);
                case ExeUnitCommand.Run:
                    return this.Worker.Run(command, 0);
                case ExeUnitCommand.Stop:
                    return this.Worker.Stop(command, 0);
                case ExeUnitCommand.Transfer:
                    return this.Worker.Transfer(command, 0);
                case ExeUnitCommand.Unknown:
                default:
                    throw new Exception($"Unknown command {command.Command}");
            }
        }

        protected ExeUnitCommand ParseCommand(string command)
        {
            switch(command?.ToUpper())
            {
                case "DEPLOY":
                    return ExeUnitCommand.Deploy;
                case "START":
                    return ExeUnitCommand.Start;
                case "RUN":
                    return ExeUnitCommand.Run;
                case "STOP":
                    return ExeUnitCommand.Stop;
                case "TRANSFER":
                    return ExeUnitCommand.Transfer;
                default:
                    return ExeUnitCommand.Unknown;
            }
        }

        public ExeUnitStateDetails GetStateDetails()
        {

            var result = new ExeUnitStateDetails()
            {
                ActivityId = this.Activity?.Id,
                CurrentUsage = this.Worker.UsageVector.Select(vec => (decimal)vec).ToList(),
                State = this.Worker.State
            };

            return result;
        }
    }
}
