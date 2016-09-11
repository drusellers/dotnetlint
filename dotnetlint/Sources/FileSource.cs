using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint.Sources
{
    public class FileSource : Source
    {
        private string _input;

        public FileSource(string input)
        {
            _input = input;
        }

        public Task<IEnumerable<TextAndPath>> Get()
        {
            IEnumerable<TextAndPath> result = new[]
            {
                new TextAndPath(SourceText.From(File.ReadAllText(_input)), _input)
            };

            return Task.FromResult(result);
        }

        public static bool CanHandle(string input)
        {
            return File.Exists(input);
        }
        
    }
}