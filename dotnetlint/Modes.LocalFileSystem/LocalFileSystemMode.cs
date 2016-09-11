using System;
using System.Collections.Generic;
using System.Text;
using dotnetlint.Modes.LocalFileSystem.Sources;

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

            Console.OutputEncoding = Encoding.UTF8;
            var output = Console.Out;

            WorkIt.Work(sources, lintCfg.Rules,
                (sourceText,
                    v) => opts.Format.Write(output, sourceText, v)
            );
        }
    }
}
