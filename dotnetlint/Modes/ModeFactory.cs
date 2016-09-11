using dotnetlint.Modes.GitHub;
using dotnetlint.Modes.LocalFileSystem;

namespace dotnetlint.Modes
{
    public static class ModeFactory
    {
        public static Mode GetMode(string input)
        {
            switch (input.ToUpper())
            {
                case "GITHUB":
                    return new GitHubMode();
                case "GITLAB":
                    return null;
                case "FILE":
                default:
                    return new LocalFileSystemMode();
            }
        }
    }
}