using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public enum ProviderEventType
    {
        CreateActivity,
        Exec,
        DestroyActivity,
        GetState
    }

    public class ProviderEvent
    {
        public ProviderEventType EventType { get; set; }

        public string ActivityId { get; set; }

    }
}
