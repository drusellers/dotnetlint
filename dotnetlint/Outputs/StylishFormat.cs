using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnetlint.Configuration;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint.Outputs
{
    public class StylishFormat : OutputFormat
    {
        public void Write(TextWriter output,
            TextAndPath text,
            IEnumerable<RuleViolation> violations)
        {
            output.WriteLine(text.Path);
            foreach (var v in violations)
            {
                output.WriteLine($"  {v.Line}:{v.StartPosition} {v.Disposition} {v.Message} {v.Rule}");
            }
            output.WriteLine();

            var errorCount = violations.Count(x => x.Disposition == RuleDispostion.Error);
            var warningCount = violations.Count(x => x.Disposition == RuleDispostion.Warning);
            var mark = violations.Any()
                ? "✖"
                : "check";
            output.WriteLine($"{mark} {violations.Count()} problems ({errorCount} errors, {warningCount} warnings)");
        }
    }
}
