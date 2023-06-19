using OnlineShopping.Models.DTO;
using OnlineShopping.Models;
using AutoMapper;

namespace OnlineShopping_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();


        }
    }
}
