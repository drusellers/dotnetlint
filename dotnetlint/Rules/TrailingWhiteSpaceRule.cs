using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class TrailingWhiteSpaceRule : Rule
    {
        public void Walk(SyntaxNode root)
        {
            var q = new Queue<SyntaxNodeOrToken>();

            q.Enqueue(root);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (current == null)
                    continue;

                foreach (var child in current.ChildNodesAndTokens())
                {
                    q.Enqueue(child);
                }

                Check(current);
            }
        }

        void Check(SyntaxNodeOrToken node)
        {
            var trivia = node.GetTrailingTrivia();
            for (int i = 0; i < trivia.Count; i++)
            {
                var inspect = trivia[i];
                if (inspect.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    if (i > 0 && trivia[i - 1].IsKind(SyntaxKind.WhitespaceTrivia))
                    {
                        Console.WriteLine($"Found trailing whitespace at {node.GetLocation().GetLineSpan().EndLinePosition}");
                    }
                }
            }
        }
    }
}