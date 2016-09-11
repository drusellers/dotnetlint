﻿using System;
using System.Collections.Generic;
using dotnetlint.Outputs;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionSet = new LintOptionSet();

            var filePaths = optionSet.Parse(args);

            var rules = new List<Rule>
            {
                new TrailingWhiteSpaceRule(),
                new NoNewLineAtEndOfFileRule(),
                new HasTabsRule()
            };
            var formatters = new Dictionary<string, OutputFormat>
            {
                {"compact", new CompatFormat()},
                {"visualstudio", new VisualStudioFormat()}
            };

            OutputFormat formatter = new VisualStudioFormat();
            if (formatters.ContainsKey(optionSet.Format))
            {
                formatter = formatters[optionSet.Format];
            }

            var sources = SourceFactory.BuildSources(filePaths);

            //TOOD: make this dynamic based on console or github
            var output = Console.Out;

            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    foreach (var rule in rules)
                    {
                        foreach (var v in rule.Check(sourceText.Parse()))
                        {
                            formatter.Write(output, v);
                        }
                    }
                }
            }
        }
    }
}
