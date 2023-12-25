using ProniaOnion.Application.DTOs.Color;

namespace ProniaOnion.Application.Abstraction.Services
{
    public interface IColorService
    {

        Task<ICollection<ColorItemDto>> GetAllPaginated(int page, int take);
        //Task<ColorGetDto> GetByIdAsync(int id);
        Task CreateAsync(ColorCreateDto categoryDto);
        Task UpdateAsync(int id, ColorUpdateDto updateCategoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);

    }
}
