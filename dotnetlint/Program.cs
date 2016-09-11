using System;
using System.Collections.Generic;
using System.Linq;
using dotnetlint.Modes;
using dotnetlint.Outputs;
using dotnetlint.Sources;

namespace dotnetlint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var modeKey = args.FirstOrDefault();
            var mode = ModeFactory.GetMode(modeKey);

            var lintOptions = new LintOptionSet();

            var remaining = lintOptions.Parse(args.Skip(1));
            LintConfiguration lintConfiguration = lintOptions;

            mode.Execute(lintConfiguration, remaining);
        }
    }
}
