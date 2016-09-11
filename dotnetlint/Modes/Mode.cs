using System.Collections.Generic;

namespace dotnetlint.Modes
{
    public interface Mode
    {
        void Execute(LintConfiguration lintCfg,
            IEnumerable<string> args);
    }
}
