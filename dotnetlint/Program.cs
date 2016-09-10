using System;
using System.Collections.Generic;
using System.IO;
using dotnetlint.Outputs;
using dotnetlint.Rules;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionSet = new LintOptionSet();

            var remaining = optionSet.Parse(args);

            var rules = new List<Rule>
            {
                new TrailingWhiteSpaceRule(),
                new NoNewLineAtEndOfFileRule(),
                new HasTabsRule()
            };

            foreach (var filePath in remaining)
            {
                foreach (var rule in rules)
                {
                    var st = SourceText.From(File.ReadAllText(filePath));
                    var x = CSharpSyntaxTree.ParseText(st, CSharpParseOptions.Default, filePath);
                    var root = x.GetRoot();

                    foreach (var v in rule.Check(root))
                    {
                        new CompatFormat().Write(v);
                    }
                }
            }
        }
    }
}
