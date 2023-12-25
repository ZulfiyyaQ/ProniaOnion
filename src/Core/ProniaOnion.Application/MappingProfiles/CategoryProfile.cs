using AutoMapper;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<Category, IncludeCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();

        }
    }
}
