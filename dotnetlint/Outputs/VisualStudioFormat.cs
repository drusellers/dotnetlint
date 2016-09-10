using System;

namespace dotnetlint.Outputs
{
    public class VisualStudioFormat : OutputFormatter
    {
        public void Write(RuleViolation v)
        {
            Console.WriteLine($" {v.FileName}({v.Line},{v.StartPosition}): {v.Rule}");
        }
    }
}