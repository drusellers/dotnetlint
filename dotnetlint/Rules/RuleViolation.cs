using dotnetlint.Modes.GitHub;

namespace dotnetlint.Rules
{
    public class RuleViolation
    {
        public RuleViolation(string rule,
            string fileName,
            int line,
            int startPosition,
            RuleDispostion dispo,
            string message,
            GithubData data)
        {
            Rule = rule;
            FileName = fileName;
            Line = line;
            StartPosition = startPosition;
            Disposition = dispo;
            Message = message;
            Github = data;
        }

        public string FileName { get; }
        public int Line { get; }
        public string Rule { get; }
        public int StartPosition { get; }
        public RuleDispostion Disposition { get; }
        public string Message { get; }
        public GithubData Github { get; }

    }
}