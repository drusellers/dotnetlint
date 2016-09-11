using Microsoft.CodeAnalysis.Text;

namespace dotnetlint
{
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