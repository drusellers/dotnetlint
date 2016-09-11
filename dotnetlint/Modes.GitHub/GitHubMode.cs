using System;
using System.Collections.Generic;
using dotnetlint.Modes.GitHub.Sources;
using dotnetlint.Outputs;
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

            WorkIt.Work(sources, lintCfg.Rules, (sourceText,
                    violations) =>
                {
                    new GitHubPrFormat(ghClient).Write(Console.Out, sourceText, violations);
                    lintCfg.Format.Write(Console.Out, sourceText, violations);
                });
        }
    }
}
