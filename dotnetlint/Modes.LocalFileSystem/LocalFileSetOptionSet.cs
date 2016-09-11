using System.Collections.Generic;
using dotnetlint.Configuration;
using dotnetlint.Outputs;

namespace dotnetlint.Modes.LocalFileSystem
{
    public class LocalFileSetOptionSet :
        OptionSet,
        LocalFileSetConfiguration
    {
        readonly IDictionary<string, OutputFormat> _outputs = new Dictionary<string, OutputFormat>
        {
            {"compact", new CompatFormat()},
            {"visualstudio", new VisualStudioFormat()}
        };

        public LocalFileSetOptionSet()
        {
            Add<string>("f|format", "Select a format", x => Format = _outputs[x]);
        }

        public OutputFormat Format { get; set; }
    }

    public interface LocalFileSetConfiguration
    {
        OutputFormat Format { get; }
    }
}
