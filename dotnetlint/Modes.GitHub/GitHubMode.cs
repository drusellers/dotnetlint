using System.Collections.Generic;
using dotnetlint.Modes.GitHub.Sources;
using Octokit;

namespace dotnetlint.Modes.GitHub
{
    public class GitHubMode : Mode
    {
        public void Execute(LintConfiguration lintCfg,
            IEnumerable<string> args)
        {
            var gho = new GitHubOptionSet();
            var remaining = gho.Parse(args);

            var ghClient = new GitHubClient(new ProductHeaderValue("dotnetlint"));
            //TOOD: AUTH

            var sources = SourceFactory.BuildSources(ghClient, remaining);

            WorkIt.Work(sources, lintCfg.Rules, (sourceText, v) =>
            {
                ghClient.PullRequest.Comment.Create(sourceText.Github.Owner,
                          sourceText.Github.Repo,
                          sourceText.Github.PR,
                          new PullRequestReviewCommentCreate(v.Message,
                              v.Github.Sha,
                              sourceText.Path,
                              v.Line));
            });
        }
    }
}
