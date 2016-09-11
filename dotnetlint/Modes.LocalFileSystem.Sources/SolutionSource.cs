using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dotnetlint.Sources;
using Microsoft.CodeAnalysis.MSBuild;

namespace dotnetlint.Modes.LocalFileSystem.Sources
{
    public class SolutionSource : Source
    {
        readonly string _input;

        public SolutionSource(string input)
        {
            _input = input;
        }

        public async Task<IEnumerable<TextAndPath>> Get()
        {
            var slnPath = Path.GetFullPath(_input);
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(slnPath);

            var results = new List<TextAndPath>();
            foreach (var project in solution.Projects)
            {
                foreach (var document in project.Documents)
                {
                    if (document.SupportsSyntaxTree)
                    {
                        results.Add(new TextAndPath(document.GetTextAsync().Result,
                            document.FilePath,
                            null));
                    }
                }
            }

            return results;
        }

        public static bool CanHandle(string input)
        {
            return File.Exists(input)
                   && input.EndsWith(".sln");
        }
    }
}
