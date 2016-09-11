using System.Collections.Generic;
using dotnetlint.Outputs;
using dotnetlint.Rules;
using dotnetlint.Sources;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint
{
    internal class Program
    {
        private static void Main(string[] args)
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
                formatter = formatters[optionSet.Format];

            var sources = SourceFactory.BuildSources(filePaths);
            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    var syntaxTree = CSharpSyntaxTree.ParseText(sourceText.Source, CSharpParseOptions.Default,
                        sourceText.Path);
                    var root = syntaxTree.GetRoot();

                    foreach (var rule in rules)
                    {
                        foreach (var v in rule.Check(root))
                        {
                            formatter.Write(v);
                        }
                    }
                }
            }
               
        }
    }
}