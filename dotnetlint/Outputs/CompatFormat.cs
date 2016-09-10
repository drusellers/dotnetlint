﻿using System;

namespace dotnetlint.Outputs
{
    public class CompatFormat : OutputFormatter
    {
        public void Write(RuleViolation v)
        {
            Console.WriteLine($"{v.FileName}: line {v.Line}, col {v.StartPosition}, {v.Disposition}: {v.Message} ({v.Rule})");
        }
    }
}