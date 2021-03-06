﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dotnetlint.Sources;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint.Modes.LocalFileSystem.Sources
{
    public class FileSource : Source
    {
        readonly string _input;

        public FileSource(string input)
        {
            _input = input;
        }

        public Task<IEnumerable<TextAndPath>> Get()
        {
            IEnumerable<TextAndPath> result = new[]
            {
                new TextAndPath(SourceText.From(File.ReadAllText(_input)), _input, null)
            };

            return Task.FromResult(result);
        }

        public static bool CanHandle(string input)
        {
            return File.Exists(input);
        }
    }
}
