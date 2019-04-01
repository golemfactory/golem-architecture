using GolemStandardSummaryGen.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GolemStandardSummaryGen
{
    public class SummaryGenerator
    {
        public string TargetFileName { get; set; }

        /// <summary>
        /// Namespace Summary recordss listed by namespace name.
        /// </summary>
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

            var toc = this.GenerateToC(this.Namespaces.Values);

            var page = new GolemStandardCheatSheet(this.Namespaces, toc);
            String pageContent = page.TransformText();
            System.IO.File.WriteAllText(this.TargetFileName, pageContent);

        }

        /// <summary>
        /// Return Table of Contents lists, indexed by category names.
        /// </summary>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        protected IDictionary<string, IList<ToCEntry>> GenerateToC(ICollection<NamespaceSummary> namespaces)
        {
            var result = new Dictionary<string, IList<ToCEntry>>();

            foreach (var category in namespaces.GroupBy(ns => ns.Category))
            {
                var items = new List<ToCEntry>();
                result.Add(category.Key, items);
                items.Add(new ToCEntry()
                {
                    Label = category.Key,
                    Level = 0
                });

                string[] nsTokens = new string[0];

                List<string> tokenStack = new List<string>();

                foreach(var ns in category.OrderBy(name => name.Name))
                {
                    var tokens = ns.Name.Split(".");

                    for(int i=0; i<tokens.Length; i++)
                    {
                        if(tokenStack.Count > i)
                        {
                            if(tokens[i] == tokenStack[i])
                            {

                            }
                            else
                            {
                                tokenStack.RemoveRange(i, tokenStack.Count - i);
                                tokenStack.Add(tokens[i]);
                                items.Add(new ToCEntry()
                                {
                                    Label = tokens[i],
                                    Level = i + 1,
                                    RelativePath = i == tokens.Length - 1 ? ns.RelativePath : null
                                });
                            }
                        }
                        else
                        {
                            tokenStack.Add(tokens[i]);
                            items.Add(new ToCEntry()
                            {
                                Label = tokens[i],
                                Level = i + 1,
                                RelativePath = i == tokens.Length - 1 ? ns.RelativePath : null
                            });
                        }
                    }

                }
            }

            return result;
        }

    }
}
