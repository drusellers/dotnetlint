using dotnetlint.Configuration;

namespace dotnetlint.Modes.GitHub
{
    public class GitHubOptionSet :
        OptionSet,
        GitHubConfiguration
    {
        public GitHubOptionSet()
        {
            Address = "https://api.github.com/";

            Add<string>("a|address", "Address of the GitHub Enterprise Instance", a => Address = a);
        }

        public string Address { get; set; }
    }

    public interface GitHubConfiguration
    {
        string Address { get; }
    }
}
