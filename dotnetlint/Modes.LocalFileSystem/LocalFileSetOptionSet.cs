using dotnetlint.Configuration;

namespace dotnetlint.Modes.LocalFileSystem
{
    public class LocalFileSetOptionSet :
        OptionSet,
        LocalFileSetConfiguration
    {
    }

    public interface LocalFileSetConfiguration
    {
    }
}
