using System.Collections.Generic;
using dotnetlint.Configuration;
using dotnetlint.Outputs;
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


            Format = new StylishFormat();
            Add<string>("f|format=", "Select a format", x => Format = SafeGet(x));
        }

        public bool Verbose { get; set; }
        public bool Help { get; set; }
        public OutputFormat Format { get; set; }

        public IEnumerable<Rule> Rules => new List<Rule>
            {
                new TrailingWhiteSpaceRule(),
                new NoNewLineAtEndOfFileRule(),
                new HasTabsRule()
            };

        readonly IDictionary<string, OutputFormat> _outputs = new Dictionary<string, OutputFormat>
        {
            {"compact", new CompatFormat()},
            {"visualstudio", new VisualStudioFormat()},
            {"stylish", new StylishFormat() }
        };
        OutputFormat SafeGet(string key)
        {
            if (_outputs.ContainsKey(key))
            {
                return _outputs[key];
            }

            return _outputs["stylish"];
        }
    }
}
