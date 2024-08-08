using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;

namespace Bookstore.API.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() 
        {
            CreateMap<Authors, AuthorsDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));


           // CreateMap<Entities.Authors, Models.AuthorsDto>();
            CreateMap<Models.AuthorsForCreationDto, Entities.Authors>();
            CreateMap<Models.AuthorsForUpdateDto, Entities.Authors>();
            CreateMap<Entities.Authors, Models.AuthorsForUpdateDto>();

        }
    }
}