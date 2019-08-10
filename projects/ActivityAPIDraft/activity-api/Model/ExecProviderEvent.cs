using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class ExecProviderEvent : ProviderEvent
    {
        public ExeScriptBatch ExeScript { get; set; }
    }
}
