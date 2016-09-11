using dotnetlint.Modes;

namespace dotnetlint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mode mode;
            var remaining = ModeFactory.GetMode(args, out mode);

            var lintOptions = new LintOptionSet();

            remaining = lintOptions.Parse(remaining);
            LintConfiguration lintConfiguration = lintOptions;

            mode.Execute(lintConfiguration, remaining);
        }
    }
}
