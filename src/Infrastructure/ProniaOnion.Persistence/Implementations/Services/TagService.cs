using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false, IsDeleted: false).ToListAsync();

            ICollection<TagItemDto> tagDtos = _mapper.Map<ICollection<TagItemDto>>(tags);

            return tagDtos;
        }
        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id, includes: $"{nameof(Tag.ProductTags)}.{nameof(ProductTag.Product)}");
            if (item == null) throw new Exception("Not Found");

            GetTagDto dto = _mapper.Map<GetTagDto>(item);

            return dto;
        }


        public async Task CreateAsync(TagCreateDto TagCreateDto)
        {
            bool result = await _repository.IsExistsAsync(c => c.Name == TagCreateDto.Name);
            if (result) throw new Exception("Already exist");
            await _repository.AddAsync(_mapper.Map<Tag>(TagCreateDto)
            );

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagUpdateDto updateTagDto)
        {
            bool result = await _repository.IsExistsAsync(c => c.Name == updateTagDto.Name);
            if (result) throw new Exception("Already exist");
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            _mapper.Map(updateTagDto, tag);

            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not Found");
            _repository.SoftDelete(tag);
            await _repository.SaveChangesAsync();
        }

    }
}
