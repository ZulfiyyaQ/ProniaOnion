using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.Abstraction.Services
{
    public interface ITagService
    {
        Task<ICollection<TagItemDto>> GetAllPaginated(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(TagCreateDto tagDto);
        Task UpdateAsync(int id, TagUpdateDto updatetagDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
