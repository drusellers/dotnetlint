using System;
using System.Collections.Generic;
using dotnetlint.Modes.LocalFileSystem.Sources;
using dotnetlint.Sources;

namespace dotnetlint.Modes.LocalFileSystem
{
    public class LocalFileSystemMode : Mode
    {
        public void Execute(LintConfiguration lintCfg,
            IEnumerable<string> args)
        {
            var opts = new LocalFileSetOptionSet();
            var remaining = opts.Parse(args);

            var sources = SourceFactory.BuildSources(remaining);

            var output = Console.Out;

            WorkIt.Work(sources, lintCfg.Rules, (sourceText, v) =>
                    opts.Format.Write(output, v));
        }
    }
}
