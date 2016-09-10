using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnetlint.Rules;

namespace dotnetlint
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionSet = new LintOptionSet();

            var remaining = optionSet.Parse(args);

            var rules = new List<Rule>();
            rules.Add(new TrailingWhiteSpaceRule());

            foreach (var filePath in remaining)
            {
                foreach (var rule in rules)
                {
                    var x = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(File.ReadAllText(filePath));
                    var root = x.GetRoot().ChildNodes().First();
                    rule.Walk(root);
                }
            }
        }
    }
}
