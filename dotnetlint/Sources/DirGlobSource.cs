using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint.Sources
{
    public class DirGlobSource : Source
    {
        private string _input;

        public DirGlobSource(string input)
        {
            _input = input;
        }

        public Task<IEnumerable<TextAndPath>> Get()
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, _input, SearchOption.AllDirectories);
            var result = files.Select(f => new TextAndPath(SourceText.From(File.ReadAllText(f)), f));
            return Task.FromResult(result);
        }

        public static bool CanHandle(string input)
        {
            return input.Contains("*");
        }
    }
}