using System.Collections.Generic;
using dotnetlint.Configuration;
using dotnetlint.Rules;

namespace dotnetlint
{
    public class LintOptionSet :
        OptionSet,
        LintConfiguration
    {
        public LintOptionSet()
        {
            Add<string>("v|verbose", "Verbose output", x => Verbose = x != null);
            Add<string>("h|help", "Display this help and exit", x => Help = x != null);
        }

        public bool Verbose { get; set; }
        public bool Help { get; set; }
        public IEnumerable<Rule> Rules => new List<Rule>
            {
                new TrailingWhiteSpaceRule(),
                new NoNewLineAtEndOfFileRule(),
                new HasTabsRule()
            };
    }
}
