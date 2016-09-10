namespace dotnetlint
{
    public class LintOptionSet : 
        OptionSet,
        LintConfiguration
    {
        public LintOptionSet()
        {
            Format = "visualstudio";

            Add<string>("v|verbose", "Verbose output", x => Verbose = x != null);
            Add<string>("h|help", "Display this help and exit", x => Help = x != null);
            Add<string>("f|format", "Select a format", x => Format = x);
        }

        public bool Verbose { get; set; }
        public bool Help { get; set; }
        public string Format { get; set; }
    }
}