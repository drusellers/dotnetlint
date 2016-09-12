namespace dotnetlint.Modes.LocalFileSystem.Sources
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using dotnetlint.Sources;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

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
                var compilationData = await project.GetCompilationAsync();
                var diagnosticData = compilationData.GetDiagnostics()
                                                    .Where(i => i.Severity != DiagnosticSeverity.Hidden);

                foreach (var document in project.Documents)
                {
                    if (document.SupportsSyntaxTree)
                    {
                        results.Add(new TextAndPath(await document.GetTextAsync(),
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
