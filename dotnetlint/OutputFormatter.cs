namespace dotnetlint
{
    public interface OutputFormatter
    {
        void Write(RuleViolation v);
    }
}