using GolemStandardSummaryGen.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GolemStandardSummaryGen
{
    public class SummaryGenerator
    {
        public string TargetFileName { get; set; }
        public IDictionary<string, NamespaceSummary> Namespaces { get; set; }

        public SummaryGenerator(IDictionary<string, NamespaceSummary> namespaces, string targetFileName)
        {
            this.Namespaces = namespaces;
            this.TargetFileName = targetFileName;
        }

        public void Process()
        {
            // translate the namespace paths

            foreach(var ns in this.Namespaces.Values)
            {
                ns.RelativePath = Path.GetRelativePath(Path.GetDirectoryName(this.TargetFileName), ns.RelativePath).Replace(Path.DirectorySeparatorChar, '/');
            }
            
            var page = new GolemStandardCheatSheet(this.Namespaces);
            String pageContent = page.TransformText();
            System.IO.File.WriteAllText(this.TargetFileName, pageContent);

        }

    }
}
