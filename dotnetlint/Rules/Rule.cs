using Microsoft.CodeAnalysis;

namespace dotnetlint.Rules
{
    public interface Rule
    {
        void Walk(SyntaxNode root);
    }
}