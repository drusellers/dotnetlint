using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dotnetlint.Sources;
using Microsoft.CodeAnalysis.Text;
using Octokit;

namespace dotnetlint.Modes.GitHub.Sources
{
    public class GithubPrSource : Source
    {
        readonly Uri _input;
        readonly IGitHubClient _client;

        public GithubPrSource(IGitHubClient client, string input)
        {
            _client = client;
            _input = new Uri(input);
        }

        public async Task<IEnumerable<TextAndPath>> Get()
        {
            var parts = _input.PathAndQuery.Split('/');
            var owner = parts[2];
            var repo = parts[3];
            var number = int.Parse(parts[5]);
            var files = await _client.PullRequest.Files(owner, repo, number);

            var result = new List<TextAndPath>();
            foreach (var file in files)
            {
                var source = await _client.Git.Blob.Get(owner, repo, file.Sha);
                
                var b = Convert.FromBase64String(source.Content);
                var content = Encoding.UTF8.GetString(b);
                result.Add(new TextAndPath(SourceText.From(content), file.FileName, new GithubData(owner, repo, number, file.Sha)));
            }

            return result;
        }

        public static bool CanHandle(string input)
        {
            Uri ignored;

            return input.ToLower().Contains("http")
                   && input.ToLower().Contains("github")
                   && input.ToLower().Contains("pulls")
                   && Regex.IsMatch(input, @"pulls/\d+/files")
                   && Uri.TryCreate(input, UriKind.Absolute, out ignored);
        }
    }
}
