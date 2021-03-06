﻿using System;
using System.Linq;

namespace GolemStandardSummaryGen
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Usage: GolemStandardSummaryGet <target summary file name> <standard folder root1> (<standard folder root2> ... <standard folder rootn>) ");
                return;
            }

            var reader = new StandardHierarchyReader(args.Skip(1).ToArray());

            var namespaces = reader.Process();

            var generator = new SummaryGenerator(namespaces, args[0]);

            generator.Process();

            namespaces.Values.SelectMany(ns => ns.Properties.Select(prop => prop.FullName))
                .OrderBy(propName => propName)
                .ToList()
                .ForEach(propName => Console.WriteLine(propName));

            Console.ReadKey();

        }
    }
}
