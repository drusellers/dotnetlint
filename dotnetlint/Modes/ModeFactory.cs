using System.Collections.Generic;
using System.Linq;
using dotnetlint.Modes.GitHub;
using dotnetlint.Modes.LocalFileSystem;

namespace dotnetlint.Modes
{
    public static class ModeFactory
    {
        public static IEnumerable<string> GetMode(string[] args,
            out Mode mode)
        {
            if (args.Any())
            {
                var m = args[0].ToUpper();
                if (new HashSet<string> {"FILE", "GITLAB", "GITHUB"}.Contains(m))
                {
                    if (m == "GITHUB")
                    {
                        mode = new GitHubMode();
                        return args.Skip(1);
                    }
                    if (m == "GITLAB")
                    {
                        mode = null;
                        return args.Skip(1);
                    }

                    mode = new LocalFileSystemMode();
                    return args.Skip(1);
                }
            }

            mode = new LocalFileSystemMode();
            return args;
        }
    }
}
