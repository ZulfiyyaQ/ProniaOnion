using ProniaOnion.Application.Dtos.Product;

namespace ProniaOnion.Application.DTOs.Color
{
    public record ColorGetDto(int id, string name, ICollection<IncludeProductDto> products);
}
