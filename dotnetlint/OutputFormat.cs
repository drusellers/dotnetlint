using System.IO;

namespace dotnetlint
{
    public interface OutputFormat
    {
        void Write(TextWriter output, RuleViolation v);
    }
}