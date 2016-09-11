using System;
using System.Collections.Generic;
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

            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    foreach (var rule in lintCfg.Rules)
                    {
                        foreach (var v in rule.Check(sourceText.Parse()))
                        {
                            opts.Format.Write(output, v);
                        }
                    }
                }
            }
        }
    }
}