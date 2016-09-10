using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class NoNewLineAtEndOfFileRule : Rule
    {
        public IEnumerable<RuleViolation> Check(SyntaxNode root)
        {
            var x = root.DescendantTrivia(descendIntoTrivia: true).ToList();
            var count = x.Count;

            var secondToLast = x[count - 2];
            var last = x[count - 1];

            if (!secondToLast.IsKind(SyntaxKind.EndOfLineTrivia) && last.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                var fileLinePositionSpan = root.GetLocation().GetLineSpan();
                yield return new RuleViolation(nameof(NoNewLineAtEndOfFileRule),
                    fileLinePositionSpan.Path,
                    fileLinePositionSpan.StartLinePosition.Line,
                    fileLinePositionSpan.StartLinePosition.Character,
                    RuleDispostion.Warning,
                    "No new line at end of file");
            }
        }
    }
}