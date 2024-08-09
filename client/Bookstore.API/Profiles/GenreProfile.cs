using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;

namespace Bookstore.API.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile() 
        {
            CreateMap<Genres, GenresDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<Genres, GenresNameDto> ();

            CreateMap<GenresForCreationDto, Genres>();
        }
    }
}