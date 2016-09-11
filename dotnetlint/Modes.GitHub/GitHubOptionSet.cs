using dotnetlint.Configuration;

namespace dotnetlint.Modes.GitHub
{
    public class GitHubOptionSet :
        OptionSet,
        GitHubConfiguration
    {
    }

    public interface GitHubConfiguration
    {
    }
}
