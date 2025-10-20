using AutoMapper;

namespace BestPracticesJWT.Service.Mappers;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(config =>
        {
            config.AddProfile<DtoMapper>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}
