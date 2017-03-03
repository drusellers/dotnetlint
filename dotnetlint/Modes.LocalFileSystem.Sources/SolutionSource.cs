// Copyright 2016 Dru Sellers, Keith Dahlby, et. al.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace dotnetlint.Modes.LocalFileSystem.Sources
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using dotnetlint.Sources;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
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
                var c = (CSharpCompilationOptions) project.CompilationOptions;
                var nc = c.WithGeneralDiagnosticOption(ReportDiagnostic.Warn);

                var compilationData = await project.GetCompilationAsync();
                var diagnosticData = compilationData.GetDiagnostics()
                                                    //.Where(i => i.Severity != DiagnosticSeverity.Hidden)
                                                    ;

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
