using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public enum ExeUnitState
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

    public class ExeUnitStateDetails
    {
        /// <summary>
        /// ExeUnit State
        /// </summary>
        public ExeUnitState State { get; set; }

        /// <summary>
        /// Additional, arbitrary attributes, which may carry any set of key-value pairs, 
        /// so that ExeUnit can notify the observers with additional specific details.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }
    }
}
