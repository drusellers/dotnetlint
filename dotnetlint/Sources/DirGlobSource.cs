using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public IEnumerable<TextAndPath> Get()
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, _input, SearchOption.AllDirectories);
            return files.Select(f => new TextAndPath(SourceText.From(File.ReadAllText(f)), f));
        }

        public static bool CanHandle(string input)
        {
            return input.Contains("*");
        }
    }
}