using System.Collections.Generic;
using dotnetlint.Rules;

namespace dotnetlint
{
    public interface LintConfiguration
    {
        bool Verbose { get; }
        bool Help { get; }
        IEnumerable<Rule> Rules { get; }
    }
}
