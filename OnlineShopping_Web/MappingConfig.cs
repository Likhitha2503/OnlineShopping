using AutoMapper;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Models;

namespace OnlineShopping_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();



        }
    }
}
