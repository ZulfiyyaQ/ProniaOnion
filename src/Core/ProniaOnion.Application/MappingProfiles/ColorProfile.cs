using AutoMapper;
using ProniaOnion.Application.DTOs.Color;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorItemDto>().ReverseMap();
            CreateMap<ColorCreateDto, Color>();
            CreateMap<ColorUpdateDto, Color>().ReverseMap();
            CreateMap<Color, IncludeColorDto>().ReverseMap();
            CreateMap<Color, ColorGetDto>().ReverseMap();

        }
    }
}
