using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dotnetlint.Sources;
using Microsoft.CodeAnalysis.Text;
using Octokit;

namespace dotnetlint.Modes.GitHub.Sources
{
    public class GithubBlobSource : Source
    {
        readonly Uri _input;

        public GithubBlobSource(string input)
        {
            _input = new Uri(input);
        }

        public async Task<IEnumerable<TextAndPath>> Get()
        {
            var k = new GitHubClient(ProductHeaderValue.Parse("dotnetlint"));
            var parts = _input.PathAndQuery.Split('/');
            var owner = parts[2];
            var repo = parts[3];
            var sha = parts[6];
            var x = await k.Git.Blob.Get(owner, repo, sha);
            var b = Convert.FromBase64String(x.Content);
            var content = Encoding.UTF8.GetString(b);
            return new[]
            {
                new TextAndPath(SourceText.From(content), _input.ToString(), new GithubData(owner, repo, 0, sha))
            };
        }

        public static bool CanHandle(string input)
        {
            Uri ignored;

            return input.ToLower().Contains("http")
                   && input.ToLower().Contains("github")
                   && input.ToLower().Contains("blob")
                   && Uri.TryCreate(input, UriKind.Absolute, out ignored);
        }
    }
}
