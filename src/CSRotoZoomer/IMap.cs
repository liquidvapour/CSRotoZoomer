namespace CSRotoZoomer
{
    public interface IMap<in TInput, out TOutput>
    {
        TOutput MapToUint32ArrayFrom(TInput srcImage);
    }
}