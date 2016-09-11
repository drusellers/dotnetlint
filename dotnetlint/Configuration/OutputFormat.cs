using System.Collections.Generic;
using System.IO;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint.Configuration
{
    public interface OutputFormat
    {
        void Write(TextWriter output,
            TextAndPath text,
            IEnumerable<RuleViolation> violations);
    }
}
