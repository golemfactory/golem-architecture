using Golem.Provider.ActivityControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public enum ActivityState
    {
        New,
        Deploying,
        Ready,
        Starting,
        Active,
        Unresponsive,
        Suspending,
        Resuming,
        Terminated
    }

    public class Activity
    {
        public string Id { get; set; }

        public ActivityState State { get; protected set; }

        /// <summary>
        /// The Agreement which is a basis for this activity.
        /// Important as it includes the specifications for the ExeUnit.
        /// </summary>
        public Agreement Agreement { get; set; }

        public IExeUnit ExeUnitInstance { get; set; }

        public void SetState(ActivityState state)
        {
            this.State = state;
        }



    }
}
