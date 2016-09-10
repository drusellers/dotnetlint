namespace dotnetlint
{
    public interface LintConfiguration
    {
        bool Verbose { get; }
        bool Help { get; }  
        string Format { get; }  
    }
}