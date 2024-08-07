using AutoMapper;

namespace Bookstore.API.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() 
        { 
            CreateMap<Entities.Authors, Models.AuthorsDto>();
            CreateMap<Models.AuthorsForCreationDto, Entities.Authors>();
            CreateMap<Models.AuthorsForUpdateDto, Entities.Authors>();
            CreateMap<Entities.Authors, Models.AuthorsForUpdateDto>();
        }
    }
}