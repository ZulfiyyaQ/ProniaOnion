using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Application.DTOs.Products
{
    public record ProductItemDto(int Id,string Name,decimal Price,string SKU,string? Description,int CategoryId,  IncludeCategoryDto category);

   
}
