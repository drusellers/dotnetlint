namespace dotnetlint
{
    public delegate void OptionAction<TKey, TValue>(TKey key,
        TValue value);
}