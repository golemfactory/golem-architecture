using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class CreateActivityProviderEvent : ProviderEvent
    {
        /// <summary>
        /// ID Agreement within which the Activity is being created.
        /// </summary>
        public string AgreementId { get; set; }
    }
}
