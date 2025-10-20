using AutoMapper;
using BestPracticesJWT.Core.Dtos;
using BestPracticesJWT.Core.Entities;

namespace BestPracticesJWT.Service.Mappers;

public class DtoMapper : Profile
{
    public DtoMapper()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<AppUserDto, AppUser>().ReverseMap();
    }
}
