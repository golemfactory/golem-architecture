using System;
using System.Collections.Generic;
using System.Text;
using Golem.MarketApi.Client.Swagger.Api;
using Golem.Provider.Entities;

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

        public ActivityController(
            IProviderGatewayApi activityApi,
            IExeUnitFactory exeUnitFactory)
        {
            this.ActivityApi = activityApi;
            this.ExeUnitFactory = exeUnitFactory;
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
        /// <param name="agreement"></param>
        /// <returns></returns>
        protected Activity CreateActivity(Agreement agreement)
        {
            // ...should we validate the Agreement again here???
            // ...should we consult ResourceManager to pre-reserve the resources???

            // All is confirmed, 

            throw new NotImplementedException();
        }
    }
}
