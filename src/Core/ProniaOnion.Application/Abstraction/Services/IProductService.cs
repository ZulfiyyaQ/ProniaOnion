using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductItemDto>> GetAllPaginated(int page, int take);
        Task<ProductGetDto> GetByIdAsync(int id);
        //Task<ICollection<ProductItemDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateDto prdocutDto);
        //Task UpdateAsync(int id, ProductUpdateDto updateproductDto);
        //Task DeleteAsync(int id);
    }
}
