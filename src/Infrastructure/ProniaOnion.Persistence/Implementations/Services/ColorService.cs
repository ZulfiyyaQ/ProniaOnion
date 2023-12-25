using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Color;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ICollection<ColorItemDto>> GetAllPaginated(int page, int take)
        {

            List<Color> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            var dtos = _mapper.Map<List<ColorItemDto>>(categories);
            return dtos;
        }
        public async Task CreateAsync(ColorCreateDto ColorCreateDto)
        {
            bool result = await _repository.IsExistsAsync(c => c.Name == ColorCreateDto.Name);
            if (result) throw new Exception("Already exist");
            await _repository.AddAsync(_mapper.Map<Color>(ColorCreateDto));

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ColorUpdateDto ColorUpdateDto)
        {
            Color Color = await _repository.GetByIdAsync(id);

            if (Color == null) throw new Exception("Not Found");
            bool result = await _repository.IsExistsAsync(c => c.Name == ColorUpdateDto.Name);
            if (result) throw new Exception("Already exist");

            _mapper.Map(ColorUpdateDto, Color);

            _repository.Update(Color);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Color Color = await _repository.GetByIdAsync(id);

            if (Color == null) throw new Exception("Not found");

            _repository.Delete(Color);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Color Color = await _repository.GetByIdAsync(id);
            if (Color == null) throw new Exception("Not Found");
            _repository.SoftDelete(Color);
            await _repository.SaveChangesAsync();
        }

        //public async Task<ColorGetDto> GetByIdAsync(int id)
        //{
        //    if (id <= 0) throw new Exception("Bad Request");
        //    Color item = await _repository.GetByIdAsync(id, includes: nameof(Color.Products));
        //    if (item == null) throw new Exception("Not Found");

        //    ColorGetDto dto = _mapper.Map<ColorGetDto>(item);

        //    return dto;
        //}

    }
}
