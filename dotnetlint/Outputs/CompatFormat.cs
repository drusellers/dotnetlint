using System.IO;

namespace dotnetlint.Outputs
{
    public class CompatFormat : OutputFormat
    {
        public void Write(TextWriter output, RuleViolation v)
        {
            output.WriteLine($"{v.FileName}: line {v.Line}, col {v.StartPosition}, {v.Disposition}: {v.Message} ({v.Rule})");
        }
    }
}