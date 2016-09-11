using System;
using System.Collections.Generic;
using dotnetlint.Sources;
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

            var sources = SourceFactory.BuildSources(remaining);

            //TOOD: make this dynamic based on console or github
            var output = Console.Out;

            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    foreach (var rule in lintCfg.Rules)
                    {
                        foreach (var v in rule.Check(sourceText.Parse()))
                        {
                            var client = new GitHubClient(new ProductHeaderValue("dotnetlint"));
                            client.PullRequest.Comment.Create(sourceText.Github.Owner,
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
        }
    }
}
