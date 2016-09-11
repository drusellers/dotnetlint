﻿using System.Collections.Generic;
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
            Format = new VisualStudioFormat();
            Add<string>("f|format", "Select a format", x => Format = SafeGet(x));
        }

        public OutputFormat Format { get; set; }

        OutputFormat SafeGet(string key)
        {
            if (_outputs.ContainsKey(key))
            {
                return _outputs[key];
            }

            return _outputs["visualstudio"];
        }
    }

    public interface LocalFileSetConfiguration
    {
        OutputFormat Format { get; }
    }
}
