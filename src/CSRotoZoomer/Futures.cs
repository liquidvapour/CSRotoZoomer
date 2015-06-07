namespace CSRotoZoomer
{
    public delegate TReturn Func<in TInput, out TReturn>(TInput input);
    public delegate void Action();
}