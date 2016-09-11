using System.IO;
using dotnetlint.Configuration;
using dotnetlint.Rules;
using Octokit;

namespace dotnetlint.Outputs
{
    public class GithubPullRequestFormat : OutputFormat
    {
        public void Write(TextWriter output,
            RuleViolation v)
        {
            var client = new GitHubClient(new ProductHeaderValue("dotnetlint"));

            client.PullRequest.Comment.Create(v.Github.Owner, v.Github.Repo, v.Github.PR,
                      new PullRequestReviewCommentCreate(v.Message, v.Github.Sha, v.FileName, v.Line));
        }
    }
}
