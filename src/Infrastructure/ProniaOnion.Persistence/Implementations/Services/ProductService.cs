using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public async Task<ICollection<ProductItemDto>> GetAllPaginated(int page, int take)
        {
            ICollection<ProductItemDto> dtos = _mapper.Map<ICollection<ProductItemDto>>(await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToArrayAsync());
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
            bool result = await _repository.IsExistsAsync(c => c.Name == productDto.Name);
            if (result) throw new Exception("Already Exist");

            await _repository.AddAsync(_mapper.Map<Product>(productDto));
            await _repository.SaveChangesAsync();
        }
    }
}
