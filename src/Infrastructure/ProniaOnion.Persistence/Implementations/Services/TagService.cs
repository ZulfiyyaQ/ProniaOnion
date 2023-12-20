using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();

            ICollection<TagItemDto> tagDtos = new List<TagItemDto>();
            foreach (Tag tag in tags)
            {
                tagDtos.Add(new TagItemDto(tag.Id, tag.Name))
                 ;
                ;
            }

            return tagDtos;
        }

        //public async Task<GetTagDto> GetByIdAsync(int id)
        //{
        //    Tag tag = await _repository.GetByIdAsync(id);
        //    if (tag == null) throw new Exception("Not found");
        //    return new GetTagDto
        //    {
        //        Id = tag.Id,
        //        Name = tag.Name
        //    };
        //}

        public async Task CreateAsync(TagCreateDto TagCreateDto)
        {
            await _repository.AddAsync(new Tag
            {
                Name = TagCreateDto.Name
            });

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagUpdateDto updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            tag.Name = updateTagDto.Name;

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

    }
}
