using System;
using System.Collections.Generic;
using System.Text;

namespace GolemStandardSummaryGen.Model
{
    public class ToCEntry
    {
        
        /// <summary>
        /// Level of indentation
        /// </summary>
        public int Level { get; set; }

        public String Label { get; set; }

        public String RelativePath { get; set; }
    }
}
