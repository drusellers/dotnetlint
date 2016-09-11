using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace dotnetlint.Rules
{
    public interface Rule
    {
        IEnumerable<RuleViolation> Check(SyntaxTree tree);
    }
}
