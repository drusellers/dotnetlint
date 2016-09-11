using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace dotnetlint
{
    public interface Rule
    {
        IEnumerable<RuleViolation> Check(SyntaxNode root);
    }

    public class RuleViolation
    {
        public RuleViolation(string rule,
            string fileName,
            int line,
            int startPosition,
            RuleDispostion dispo,
            string message)
        {
            Rule = rule;
            FileName = fileName;
            Line = line;
            StartPosition = startPosition;
            Disposition = dispo;
        }

        public string FileName { get; }
        public int Line { get; }
        public string Rule { get; }
        public int StartPosition { get; }
        public RuleDispostion Disposition { get; }
        public string Message { get; }
    }

    public enum RuleDispostion
    {
        Unknown,
        Warning,
        Error
    }
}
