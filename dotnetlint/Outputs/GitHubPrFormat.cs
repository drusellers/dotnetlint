using System.Collections.Generic;
using System.IO;
using dotnetlint.Configuration;
using dotnetlint.Rules;
using dotnetlint.Sources;
using Octokit;

namespace dotnetlint.Outputs
{
    public class GitHubPrFormat : OutputFormat
    {
        readonly IGitHubClient _client;

        public GitHubPrFormat(IGitHubClient client)
        {
            _client = client;
        }

        public void Write(TextWriter output,
            TextAndPath sourceText,
            IEnumerable<RuleViolation> violations)
        {
            if (sourceText?.Github.PR <= 0)
            {
                return;
            }

            foreach (var v in violations)
            {
                _client.PullRequest.Comment.Create(sourceText.Github.Owner,
                           sourceText.Github.Repo,
                           sourceText.Github.PR,
                           new PullRequestReviewCommentCreate(v.Message,
                               v.Github.Sha,
                               sourceText.Path,
                               v.Line));
            }
        }
    }
}
