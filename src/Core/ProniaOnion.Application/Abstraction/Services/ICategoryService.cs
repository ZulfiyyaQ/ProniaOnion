using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Application.Abstraction.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryItemDto>> GetAllPaginated(int page, int take);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto categoryDto);
        Task UpdateAsync(int id, CategoryUpdateDto updateCategoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        
    }
}
