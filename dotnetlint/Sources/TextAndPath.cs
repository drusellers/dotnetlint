using System;
using dotnetlint.Modes.GitHub;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint.Sources
{
    public class TextAndPath
    {
        public TextAndPath(SourceText source,
            string path,
            GithubData github)
        {
            Source = source;
            Path = path;
            Github = github;
            _parse = new Lazy<SyntaxTree>(ValueFactory);
        }

        public SourceText Source { get; }
        public string Path { get; }
        public GithubData Github { get; }

        Lazy<SyntaxTree> _parse;

        SyntaxTree ValueFactory()
        {
            return CSharpSyntaxTree.ParseText(Source,
                CSharpParseOptions.Default,
                Path);
        }

        public SyntaxTree Parse()
        {
            return _parse.Value;
        }
    }
}
