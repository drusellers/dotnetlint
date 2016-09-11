using System.IO;
using dotnetlint.Configuration;
using dotnetlint.Rules;

namespace dotnetlint.Outputs
{
    public class VisualStudioFormat : OutputFormat
    {
        public void Write(TextWriter output,
            RuleViolation v)
        {
            output.WriteLine($" {v.FileName}({v.Line},{v.StartPosition}): {v.Rule}");
        }
    }
}
