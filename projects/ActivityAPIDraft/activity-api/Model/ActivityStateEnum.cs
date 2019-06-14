using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public enum ActivityStateEnum
    {
        New,
        Deploying,
        Ready,
        Starting,
        Active,
        Unresponsive,
        Terminated
    }
}
