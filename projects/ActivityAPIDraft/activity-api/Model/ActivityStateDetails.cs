using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class ActivityStateDetails : ActivityState
    {
        /// <summary>
        /// Current usage vector
        /// </summary>
        public decimal[] CurrentUsage { get; set; }
    }
}
