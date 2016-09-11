namespace dotnetlint.Modes.GitHub
{
    public class GithubData
    {
        public GithubData(string owner,
            string repo,
            int pr,
            string sha)
        {
            Owner = owner;
            Repo = repo;
            PR = pr;
            Sha = sha;
        }

        public string Owner { get; }
        public string Repo { get; }
        public int PR { get; }
        public string Sha { get; }
    }
}