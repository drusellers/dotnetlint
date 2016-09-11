using System.Collections.Generic;

namespace dotnetlint.Sources
{
    public static class SourceFactory
    {
        public static IEnumerable<Source> BuildSources(List<string> inputs)
        {
            foreach (var input in inputs)
            {
                if (DirGlobSource.CanHandle(input))
                {
                    yield return new DirGlobSource(input);
                    continue;
                }

                if (GithubBlobSource.CanHandle(input))
                {
                    yield return new GithubBlobSource(input);
                    continue;
                }

                if (FileSource.CanHandle(input))
                {
                    yield return new FileSource(input);
                    continue;
                }

                if (GithubPrSource.CanHandle(input))
                {
                    yield return new GithubPrSource(input);
                }
            }
        }
    }
}