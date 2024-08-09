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

            CreateMap<Authors, AuthorsNameDto>();

            CreateMap<AuthorsForCreationDto, Authors>();

            CreateMap<AuthorsForUpdateDto, Authors>();

            CreateMap<Authors, AuthorsForUpdateDto>();

        }
    }
}