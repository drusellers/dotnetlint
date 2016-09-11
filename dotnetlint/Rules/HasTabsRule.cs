using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetlint.Rules
{
    public class HasTabsRule : Rule
    {
        public IEnumerable<RuleViolation> Check(SyntaxTree tree)
        {
            var root = tree.GetRoot();
            var hasTabs = root.DescendantTrivia(descendIntoTrivia: true)
                              .Where(node => node.IsKind(SyntaxKind.WhitespaceTrivia)
                                             && (node.ToString().IndexOf('\t') >= 0));

            foreach (var syntaxTrivia in hasTabs)
            {
                var fileLinePositionSpan = syntaxTrivia.GetLocation().GetLineSpan();

                yield return
                    new RuleViolation(nameof(HasTabsRule),
                        fileLinePositionSpan.Path,
                        fileLinePositionSpan.StartLinePosition.Line,
                        fileLinePositionSpan.StartLinePosition.Character,
                        RuleDispostion.Warning,
                        "Has tabs.",
                        null);
            }
        }
    }
}
