using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var filePaths = optionSet.Parse(args);

            var rules = new List<Rule>
            {
                new TrailingWhiteSpaceRule(),
                new NoNewLineAtEndOfFileRule(),
                new HasTabsRule()
            };
            var formatters = new Dictionary<string ,OutputFormat>
            {
                {"compact", new CompatFormat()},
                {"visualstudio",new VisualStudioFormat() }
            };

            OutputFormat formatter = new VisualStudioFormat();
            if (formatters.ContainsKey(optionSet.Format))
            {
                formatter = formatters[optionSet.Format];
            }

            foreach (var filePath in filePaths)
            {
                if (filePath.Contains("*"))
                {
                    var files = Directory.GetFiles(Environment.CurrentDirectory, filePath, SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        foreach (var rule in rules)
                        {
                            var st = SourceText.From(File.ReadAllText(file));
                            var x = CSharpSyntaxTree.ParseText(st, CSharpParseOptions.Default, file);
                            var root = x.GetRoot();

                            foreach (var v in rule.Check(root))
                            {
                                formatter.Write(v);
                            }
                        }
                    }
                }
                else
                {
                    if (File.Exists(filePath))
                    {   
                        foreach (var rule in rules)
                        {
                            var st = SourceText.From(File.ReadAllText(filePath));
                            var x = CSharpSyntaxTree.ParseText(st, CSharpParseOptions.Default, filePath);
                            var root = x.GetRoot();

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
}
