using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public class Agreement
    {
        public string AgreementId { get; set; }

        public Demand Demand { get; set; }

        public Offer Offer { get; set; }
    }
}
