using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Color;

namespace ProniaOnion.Application.DTOs.Products
{
    public record ProductGetDto(int id, string name, decimal Price, string SKU, string? Description, int CategoryId, IncludeCategoryDto Category, IncludeColorDto Color);
}
