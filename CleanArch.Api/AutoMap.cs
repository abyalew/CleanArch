using AutoMapper;
using CleanArch.Application.Abstractions.Interfaces;
using System.Reflection;

namespace CleanArch.Api
{
    public class AutoMap : IAutoMap
    {
        private readonly Mapper _mapper;
        public AutoMap()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(IAutoMap))));
            _mapper = new Mapper(configuration);
        }
        public TDestination MapTo<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
