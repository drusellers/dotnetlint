using System.Collections.Generic;
using System.IO;
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

        public IEnumerable<TextAndPath> Get()
        {
            yield return new TextAndPath(SourceText.From(File.ReadAllText(_input)), _input);
        }

        public static bool CanHandle(string input)
        {
            return File.Exists(input);
        }
    }
}