using System.Collections.Generic;

namespace dotnetlint.Sources
{
    public class GithubBlobSource : Source
    {
        private string _input;

        public GithubBlobSource(string input)
        {
            _input = input;
        }

        public IEnumerable<TextAndPath> Get()
        {
            throw new System.NotImplementedException();
        }

        public static bool CanHandle(string input)
        {
            return input.ToLower().Contains("http") && input.ToLower().Contains("github");
        }
    }
}