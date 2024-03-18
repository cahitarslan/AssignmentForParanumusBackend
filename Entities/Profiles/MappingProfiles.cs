using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.Product;

namespace Entities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, AddProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, GetProductDto>().ReverseMap();
    }
}
