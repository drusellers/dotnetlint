using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class NoNewLineAtEndOfFileRule : Rule
    {
        public void Walk(SyntaxNode root)
        {
            var x = root.DescendantTrivia(descendIntoTrivia: true).ToList();
            var count = x.Count;

            var secondToLast = x[count - 2];
            var last = x[count - 1];

            if (!secondToLast.IsKind(SyntaxKind.EndOfLineTrivia) && last.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                Console.WriteLine($"{root.GetLocation().GetLineSpan().Path} is missing a new line at the end");
            }

        }
    }
}