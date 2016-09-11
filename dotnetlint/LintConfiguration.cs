using System.Collections.Generic;
using dotnetlint.Configuration;
using dotnetlint.Rules;

namespace dotnetlint
{
    public interface LintConfiguration
    {
        bool Verbose { get; }
        bool Help { get; }
        IEnumerable<Rule> Rules { get; }
        OutputFormat Format { get; }
    }
}
