using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public class Proposal 
    {
        public string Id { get; set; }
        public string IssuerNodeId { get; set; }
        public string Constraints { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
