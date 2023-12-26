using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.DTOs.Products
{
    public record ProductCreateDto( string Name, decimal Price, string SKU, string? Description, int CategoryId,ICollection<int> ColorIds, ICollection<int> TagIds);
}
