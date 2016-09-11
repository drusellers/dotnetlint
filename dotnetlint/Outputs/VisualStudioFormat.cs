using System.Collections.Generic;
using System.IO;
using dotnetlint.Configuration;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint.Outputs
{
    public class VisualStudioFormat : OutputFormat
    {
        public void Write(TextWriter output,
            TextAndPath file,
            IEnumerable<RuleViolation> violations)
        {
            foreach (var v in violations)
            {
                output.WriteLine($" {v.FileName}({v.Line},{v.StartPosition}): {v.Rule}");
            }
        }
    }
}
