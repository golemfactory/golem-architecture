using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class ActivityState
    {
        public ActivityStateEnum State { get; set; }

        /// <summary>
        /// Reason for Activity termination (specified when Activity in Terminated state).
        /// </summary>
        public string Reason { get; set; }
    }
}
