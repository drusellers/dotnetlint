using System.Collections.Generic;
using System.IO;
using dotnetlint.Rules;

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
                    var x = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(File.ReadAllText(filePath));
                    var root = x.GetRoot();
                    rule.Walk(root);
                }
            }
        }
    }
}
