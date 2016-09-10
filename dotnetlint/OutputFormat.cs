namespace dotnetlint
{
    public interface OutputFormat
    {
        void Write(RuleViolation v);
    }
}