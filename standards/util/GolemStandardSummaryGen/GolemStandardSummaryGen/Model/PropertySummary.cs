using System;
using System.Collections.Generic;
using System.Text;

namespace GolemStandardSummaryGen.Model
{
    public class PropertySummary
    {
        public string Namespace { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public string Describes { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
