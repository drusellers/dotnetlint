using System.IO;
using dotnetlint.Rules;

namespace dotnetlint.Configuration
{
    public interface OutputFormat
    {
        void Write(TextWriter output,
            RuleViolation v);
    }
}
