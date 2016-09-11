using System.Collections.Generic;
using Microsoft.CodeAnalysis.Text;

namespace dotnetlint
{
    public interface Source
    {
        IEnumerable<TextAndPath> Get();
    }

    public class TextAndPath
    {
        public TextAndPath(SourceText source, string path)
        {
            Source = source;
            Path = path;
        }

        public SourceText Source { get; }
        public string Path { get; }
    }
}