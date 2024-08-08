using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;

namespace Bookstore.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Entities.Books, Models.BooksTitleDto>();

            CreateMap<Models.BooksForCreationDto, Entities.Books>();
            CreateMap<Models.BooksForUpdateDto, Entities.Books>();
            CreateMap<Entities.Books, Models.BooksForUpdateDto>();

            //CreateMap<Books, BooksDto>()
            //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        }
    }
}
