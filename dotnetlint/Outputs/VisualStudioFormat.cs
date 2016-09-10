using System;

namespace dotnetlint.Outputs
{
    public class VisualStudioFormat : OutputFormat
    {
        public void Write(RuleViolation v)
        {
            Console.WriteLine($" {v.FileName}({v.Line},{v.StartPosition}): {v.Rule}");
        }
    }
}