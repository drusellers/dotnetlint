using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace dotnetlint
{
    public interface Rule
    {
        IEnumerable<RuleViolation> Check(SyntaxNode root);
    }
}
