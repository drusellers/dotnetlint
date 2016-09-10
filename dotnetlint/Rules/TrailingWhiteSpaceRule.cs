using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class TrailingWhiteSpaceRule : Rule
    {
        public IEnumerable<RuleViolation> Check(SyntaxNode root)
        {
            var q = new Queue<SyntaxNodeOrToken>();
            var result = new List<RuleViolation>();

            q.Enqueue(root);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (current == null)
                    continue;

                foreach (var child in current.ChildNodesAndTokens())
                    q.Enqueue(child);

                result.AddRange(Check(current));
            }

            return result;
        }

        private IEnumerable<RuleViolation> Check(SyntaxNodeOrToken node)
        {
            var trivia = node.GetTrailingTrivia();
            for (var i = 0; i < trivia.Count; i++)
            {
                var inspect = trivia[i];
                if (inspect.IsKind(SyntaxKind.EndOfLineTrivia))
                    if ((i > 0) && trivia[i - 1].IsKind(SyntaxKind.WhitespaceTrivia))
                    {
                        var fileLinePositionSpan = node.GetLocation().GetLineSpan();
                        yield return new RuleViolation(nameof(TrailingWhiteSpaceRule),
                            fileLinePositionSpan.Path,
                            fileLinePositionSpan.StartLinePosition.Line,
                            fileLinePositionSpan.StartLinePosition.Character,
                            RuleDispostion.Warning,
                            "Found trailing whitespace");
                    }
            }
        }
    }
}