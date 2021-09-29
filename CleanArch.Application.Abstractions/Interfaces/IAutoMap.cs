namespace CleanArch.Application.Abstractions.Interfaces
{
    public interface IAutoMap
    {
        TDestination MapTo<TSource, TDestination>(TSource source);
        TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination);
    }
}
