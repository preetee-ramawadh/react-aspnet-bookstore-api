using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;

namespace Bookstore.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {

            CreateMap<Books, BooksDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));

            CreateMap<Books, BooksTitleDto>();

            CreateMap<BooksForCreationDto, Books>();

            CreateMap<BooksForUpdateDto, Books>();

            CreateMap<Books, BooksForUpdateDto>();

        }
    }
}
