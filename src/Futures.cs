namespace CSRotoZoomer
{
    public delegate TReturn Func<in TInput, out TReturn>(TInput input);
    internal delegate void Action<in TInputA, in TInputB>(TInputA a, TInputB b);
}