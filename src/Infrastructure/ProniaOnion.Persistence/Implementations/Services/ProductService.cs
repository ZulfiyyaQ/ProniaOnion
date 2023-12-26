using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;

        public ProductService(IProductRepository repository,
            ICategoryRepository categoryRepository,
            IColorRepository colorRepository,
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;

        public async Task<ICollection<ProductItemDto>> GetAllPaginated(int page, int take)
        {
            string[] includes = { "Category" };
            List<Product> products = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, include: includes).ToListAsync();
            var dtos = _mapper.Map<List<ProductItemDto>>(products);
            return dtos;
        }

        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Product item = await _repository.GetByIdAsync(id, includes: nameof(Product.Category));
            if (item == null) throw new Exception("Not Found");

            ProductGetDto dto = _mapper.Map<ProductGetDto>(item);

            return dto;
        }

        public async Task CreateAsync(ProductCreateDto productDto)
        {
            bool result = await _repository.IsExistsAsync(p => p.Name == productDto.Name);
            if (result) throw new Exception("Already Exist");
            if (!await _categoryRepository.IsExistsAsync(c => c.Id == productDto.CategoryId)) throw new Exception("Bele category yoxdur");

            Product product = _mapper.Map<Product>(productDto);
            product.ProductColors = new List<ProductColor>();


            foreach (var colorId in productDto.ColorIds)
            {
                if (!await _colorRepository.IsExistsAsync(c => c.Id == colorId)) throw new Exception("Already Exist");
                product.ProductColors.Add(new ProductColor
                {
                    ColorId = colorId
                });

            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            string[] include = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}" };
            Product existed = await _repository.GetByIdAsync(id, includes: include);
            if (existed is null) throw new Exception("Not found");

            if (dto.CategoryId != existed.CategoryId)
                if (!await _categoryRepository.IsExistsAsync(c => c.Id == dto.CategoryId))
                    throw new Exception("Category Not Found");

            existed = _mapper.Map(dto, existed);

            existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colid => pc.ColorId == colid)).ToList();
            foreach (var colorId in dto.ColorIds)
            {
                if (!await _colorRepository.IsExistsAsync(x => x.Id == colorId)) throw new Exception("Color not found.");
                if (!existed.ProductColors.Any(pc => pc.ColorId == colorId))
                {
                    existed.ProductColors.Add(new ProductColor
                    {
                        ColorId = colorId
                    });
                }
            }

            existed.ProductTags = existed.ProductTags.Where(pt => dto.TagIds.Any(tagid => pt.TagId == tagid)).ToList();
            foreach (var tagid in dto.TagIds)
            {
                if (!await _tagRepository.IsExistsAsync(x => x.Id == tagid)) throw new Exception("Tag not found.");
                if (!existed.ProductTags.Any(pt => pt.TagId == tagid))
                {
                    existed.ProductTags.Add(new ProductTag
                    {
                        TagId = tagid
                    });
                }
            }

            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            //Product existed = await _repository.GetByIdAsync(id, includes: nameof(Product.ProductColors));
            //if (existed == null) throw new Exception("Bele product yoxdur");

            //if (dto.CategoryId != existed.CategoryId)
            //    if (!await _categoryRepository.IsExistsAsync(c => c.Id == dto.CategoryId))
            //        throw new Exception("Bele product yoxdur");
            //existed = _mapper.Map(dto, existed);
            //existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colorid => pc.ColorId == colorid)).ToList();
            //foreach (var cid in dto.ColorIds)
            //{
            //    if (!await _colorRepository.IsExistsAsync(c => c.Id == cid)) throw new Exception("Already Exist");
            //    if (!existed.ProductColors.Any(pc => pc.ColorId == cid)) existed.ProductColors.Add(new ProductColor { ColorId = cid });
            //}
            //await _repository.AddAsync(existed);
            //await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { $" {nameof(Product.ProductColors)}",$"{nameof(Product.ProductTags)}" };
            Product item = await _repository.GetByIdAsync(id, IsDeleted: true, includes: includes);
            if (item == null) throw new Exception("Not Found");
            _repository.Delete(item);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeLeteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { "ProductColors.Color", "ProductTags.Tag"};
       
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new Exception("Not Found");

            _repository.SoftDelete(item);

            foreach (ProductColor productColor in item.ProductColors)
                productColor.IsDeleted = true;

            foreach (ProductTag productTag in item.ProductTags)
                productTag.IsDeleted = true;

            await _repository.SaveChangesAsync();
        }

        public async Task ReverseSoftDeLeteAsync(int id)
        {
            if (id < 0) throw new Exception("Bad Request");
            string[] includes = { "ProductColors.Color", "ProductTags.Tag" };
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("Not Found");

            _repository.ReverseSoftDelete(item);

            foreach (ProductColor productColor in item.ProductColors)
                productColor.IsDeleted = false;

            foreach (ProductTag productTag in item.ProductTags)
                productTag.IsDeleted = false;

            await _repository.SaveChangesAsync();
        }

    }
}



