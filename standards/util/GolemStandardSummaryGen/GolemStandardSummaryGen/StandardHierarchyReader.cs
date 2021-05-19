using GolemStandardSummaryGen.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GolemStandardSummaryGen
{
    public class StandardHierarchyReader
    {
        public string[] FolderRoots { get; set; }
        public Regex PropertyLineNoCategoryRegex { get; } = new Regex(@"^##\s+`(?<name>.+)\s*:\s*(?<type>.*)\s*`\s*$");

        public Regex PropertyLineRegex { get; } = new Regex(@"^##\s+`(?<name>.+)\s*:\s*(?<type>.*)\s*` .*\[+(?<category>.*)\]\].*$");
        public Regex DescribesLineRegex { get; } = new Regex(@"^###\s+Describes:\s*(?<describes>.*)$");

        public StandardHierarchyReader(string[] folderRoots)
        {
            this.FolderRoots = folderRoots;


        }

        public IDictionary<string, NamespaceSummary> Process()
        {
            var namespaces = new Dictionary<string, NamespaceSummary>();
            foreach(var folderRoot in this.FolderRoots)
            {
                var entries = Directory.EnumerateFileSystemEntries(folderRoot).ToList();

                this.TraverseFileSystemEntriesCollection(entries, namespaces, 0, folderRoot);
            }

            return namespaces;
        }

        public void TraverseFileSystemEntriesCollection(IEnumerable<string> entries, IDictionary<string, NamespaceSummary> namespaces, int level, string folderRoot)
        {
            foreach(var item in entries)
            {
                // check if item is a directory or file
                if (Directory.Exists(item))
                {
                    var subFolderEntries = Directory.EnumerateFileSystemEntries(item).ToList();
                    this.TraverseFileSystemEntriesCollection(subFolderEntries, namespaces, level+1, folderRoot);
                }
                else
                {
                    if(level > 0) // ignore files in root folder
                    {
                        // process file to extract properties
                        this.ProcessFile(item, namespaces, folderRoot);
                    }

                }
            }
        }

        public (string, string) DecodeNamespace(string fileName, string folderRoot)
        {
            var relPath = fileName;
            
            // remove the root folder, and strip the .md from the end.
            // then replace all slashes with dots

            string result = null;

            if (fileName.EndsWith(".md"))
            {
                result = fileName.Substring(0, fileName.Length - 3);
            }
            else
            {
                return (null, relPath);
            }

            if(result.StartsWith(folderRoot))
            {
                result = result.Substring(folderRoot.Length+1);
            }
            else
            {
                return (null, relPath);
            }

            result = result.Replace(Path.DirectorySeparatorChar, '.');

            return (result, relPath);
        }

        public string DecodeCategoryFromFolderRoot(string folderRoot)
        {
            var folders = folderRoot.Split(new char[] { Path.DirectorySeparatorChar } );

            return folders.Last();
        }

        public void AddPropertyToNamespaces(PropertySummary property, IDictionary<string, NamespaceSummary> namespaces)
        {
            namespaces[property.Namespace].Properties.Add(property);
        }

        public void ProcessFile(string fileName, IDictionary<string, NamespaceSummary> namespaces, string folderRoot)
        {

            var (nsName, nsRelPath) = this.DecodeNamespace(fileName, folderRoot);

            var ns = new NamespaceSummary()
            {
                Category = this.DecodeCategoryFromFolderRoot(folderRoot),
                Name = nsName,
                RelativePath = nsRelPath,
                Properties = new List<PropertySummary>(),
                IncludedNamespaces = new List<string>()
            };

            if (fileName.ToLower().EndsWith(".md"))
            {
                Console.WriteLine($"Processing file: {fileName}");

                using (var file = File.OpenRead(fileName))
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        // If namespace description not yet populated - check if this looks like the first header
                        if (ns.Description == null)
                        {
                            if (line.StartsWith("# ")) // OK this looks like namespace file header
                            {
                                string descLine = "";

                                // skip any empty lines that follow.
                                while (!reader.EndOfStream && String.IsNullOrWhiteSpace(descLine = reader.ReadLine())) ;

                                // read and aggregate the description
                                do
                                {
                                    ns.Description += descLine;
                                    if (!String.IsNullOrWhiteSpace(descLine))
                                    {
                                        ns.Description += " ";
                                    }
                                }
                                while (!reader.EndOfStream && !String.IsNullOrWhiteSpace(descLine = reader.ReadLine()) && !descLine.StartsWith("#"));

                                // now add the namespace to the list

                                if (!namespaces.ContainsKey(ns.Name))
                                {
                                    namespaces.Add(nsName, ns);
                                }
                            }
                        }

                        // check if line looks like Common Properties:
                        if (line.StartsWith("## ") && line.ToLower().Contains("common properties"))
                        {
                            string descLine = null;

                            // skip any empty lines that follow.
                            while (!reader.EndOfStream && String.IsNullOrWhiteSpace(descLine = reader.ReadLine())) ;

                            // read and aggregate the description
                            do
                            {
                                if (!String.IsNullOrWhiteSpace(descLine) && descLine.Trim().StartsWith("*"))
                                {
                                    ns.IncludedNamespaces.Add(descLine);
                                }
                            }
                            while (!reader.EndOfStream && !String.IsNullOrWhiteSpace(descLine = reader.ReadLine()) && !descLine.StartsWith("#"));

                        }

                        // check if line matches the "property layout" regex (either one of two formats)
                        var match = this.PropertyLineRegex.Match(line);
                        var matchNoCategory = this.PropertyLineNoCategoryRegex.Match(line);

                        if (match.Success || matchNoCategory.Success)
                        {
                            string descLine = "";

                            // if no category found, but still a property line - process the no-category match
                            if (!match.Success)
                                match = matchNoCategory;

                            var property = new PropertySummary()
                            {
                                Namespace = nsName,
                                Type = match.Groups["type"].Value,
                                FullName = match.Groups["name"].Value.Trim(),
                                Category = match.Groups["category"].Value.Trim()
                            };

                            if (!property.FullName.StartsWith(property.Namespace) &&
                                !property.FullName.StartsWith("golem." + property.Namespace))
                            {
                                Console.WriteLine($"Warning: property name {property.FullName} is not aligned with namespace {property.Namespace}");
                            }

                            // skip any empty lines that follow.
                            while (!reader.EndOfStream && String.IsNullOrWhiteSpace(descLine = reader.ReadLine())) ;

                            match = this.DescribesLineRegex.Match(descLine);

                            if (match.Success)
                            {
                                property.Describes = match.Groups["describes"].Value;

                                // skip empty lines after the "Describes: " line
                                while (!reader.EndOfStream && String.IsNullOrWhiteSpace(descLine = reader.ReadLine())) ;
                            }

                            // read and aggregate the description
                            do
                            {
                                property.Description += descLine;
                                if (!String.IsNullOrWhiteSpace(descLine))
                                {
                                    property.Description += " ";
                                }
                            }
                            while (!reader.EndOfStream && !String.IsNullOrWhiteSpace(descLine = reader.ReadLine()) && !descLine.StartsWith("#"));

                            // done processing the property, now add it to namespace

                            this.AddPropertyToNamespaces(property, namespaces);

                        }
                    }
                }

            }
        }
    }
}
