using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class HasTabsRule : Rule
    {
        public void Walk(SyntaxNode root)
        {
            var hasTabs = root.DescendantTrivia(descendIntoTrivia: true)
                .Where(node => node.IsKind(SyntaxKind.WhitespaceTrivia)
                             && node.ToString().IndexOf('\t') >= 0);

            foreach (var syntaxTrivia in hasTabs)
            {
                Console.WriteLine($"Found tabs at {syntaxTrivia.GetLocation().GetLineSpan().EndLinePosition}");
            }
        }
    }
}