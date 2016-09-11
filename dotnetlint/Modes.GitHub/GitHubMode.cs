using System;
using System.Collections.Generic;
using dotnetlint.Modes.GitHub.Sources;
using Octokit;
using Octokit.Internal;

namespace dotnetlint.Modes.GitHub
{
    public class GitHubMode : Mode
    {
        public void Execute(LintConfiguration lintCfg,
            IEnumerable<string> args)
        {
            var gho = new GitHubOptionSet();
            var remaining = gho.Parse(args);

            var ghClient = new GitHubClient(new ProductHeaderValue("dotnetlint"), 
                new InMemoryCredentialStore(Credentials.Anonymous),
                new Uri(gho.Address));

            
            var sources = SourceFactory.BuildSources(ghClient, remaining);

            WorkIt.Work(sources, lintCfg.Rules, (sourceText, violations) =>
            {
                foreach (var v in violations)
                {
                    ghClient.PullRequest.Comment.Create(sourceText.Github.Owner,
                                sourceText.Github.Repo,
                                sourceText.Github.PR,
                                new PullRequestReviewCommentCreate(v.Message,
                                    v.Github.Sha,
                                    sourceText.Path,
                                    v.Line));
                }
            });
        }
    }
}
