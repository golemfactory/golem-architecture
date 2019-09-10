using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class ExecProviderEvent : ProviderEvent
    {
        /// <summary>
        /// Batch Id
        /// </summary>
        public string BatchId { get; set; }


        public ExeScriptBatch ExeScript { get; set; }
    }
}
