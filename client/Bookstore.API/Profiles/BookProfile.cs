using AutoMapper;

namespace Bookstore.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Entities.Books, Models.BooksDto>();
            CreateMap<Models.BooksForCreationDto, Entities.Books>();
            CreateMap<Models.BooksForUpdateDto, Entities.Books>();
            CreateMap<Entities.Books, Models.BooksForUpdateDto>();
        }
    }
}
