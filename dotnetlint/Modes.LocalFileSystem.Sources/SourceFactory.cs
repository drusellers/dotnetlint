using System.Collections.Generic;
using dotnetlint.Sources;

namespace dotnetlint.Modes.LocalFileSystem.Sources
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


                if (SolutionSource.CanHandle(input))
                {
                    yield return new SolutionSource(input);
                    continue;
                }

                if (FileSource.CanHandle(input))
                {
                    yield return new FileSource(input);
                }

            }
        }
    }
}
