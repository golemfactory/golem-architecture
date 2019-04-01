using System;
using System.Collections.Generic;
using System.Text;

namespace GolemStandardSummaryGen.Model
{
    public class NamespaceSummary
    {
        public string Category { get; set; }

        /// <summary>
        /// Namespace name/path
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }

        public string RelativePath { get; set; }

        public ICollection<PropertySummary> Properties { get; set; }

        public IList<string> IncludedNamespaces { get; set; }
    }
}
