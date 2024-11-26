namespace EShopProject.Helpers;

using AutoMapper;
using EShopProject.Entities;
using EShopProject.Dtos;

public class EShopProjectMappings : Profile
{
    public EShopProjectMappings()
    {
        CreateMap<AddProductDto, ProductEntity>().ReverseMap();
        CreateMap<UpdateProductDto, ProductEntity>().ReverseMap();
        CreateMap<ProductDto, ProductEntity>().ReverseMap();
    }
}
