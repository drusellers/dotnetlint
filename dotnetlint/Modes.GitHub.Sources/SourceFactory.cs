using System.Collections.Generic;
using dotnetlint.Sources;
using Octokit;

namespace dotnetlint.Modes.GitHub.Sources
{
    public class SourceFactory
    {
        public static IEnumerable<Source> BuildSources(IGitHubClient client,
            List<string> inputs)
        {
            foreach (var input in inputs)
            {
                if (GithubBlobSource.CanHandle(input))
                {
                    yield return new GithubBlobSource(client, input);
                }

                if (GithubPrSource.CanHandle(input))
                {
                    yield return new GithubPrSource(client, input);
                }
            }
        }
    }
}
