using AutoMapper;

namespace Bookstore.API.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile() 
        {
            CreateMap<Entities.Genres, Models.GenresDto>();
            CreateMap<Models.GenresForCreationDto, Entities.Genres>();
        }
    }
}