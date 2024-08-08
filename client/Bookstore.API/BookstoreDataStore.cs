using Bookstore.API.Models;

namespace Bookstore.API
{
    public class BookstoreDataStore
    {
        public List<BooksDto> Books { get; set; }

        public List<AuthorsDto> Authors { get; set; }

        public List<GenresDto> Genres { get; set; }

        public BookstoreDataStore() 
        {
            Books = new List<BooksDto>()
            {
                new BooksDto()
                {
                    BookId = 1,
                    Title = "New York City",
                    Price = 10.99M,
                    PublicationDate = new DateOnly(2024,09,05),
                    AuthorId = 1,
                    GenreId = 1,
                    ImageUrl = "/images/books/imageunavailable.jpg"
                },
                 new BooksDto()
                {
                    BookId = 2,
                    Title = "New York City",
                    Price = 100.50M,
                    PublicationDate = new DateOnly(2021,11,05),
                    AuthorId = 2,
                    GenreId = 2,
                    ImageUrl = "/images/books/imageunavailable.jpg"
                }
            };

            Authors = new List<AuthorsDto>()
            {
                new AuthorsDto()
                {
                    Name = "M.K. Gandhi",
                    Biography = "Gandhi's biography here",
                    ImageUrl =  "/images/authors/imagesunavailable.jpg"
                },
                 new AuthorsDto()
                {
                    Name = "Paul Brunton",
                    Biography = "Paul Brunton's biography here",
                    ImageUrl =  "/images/authors/imagesunavailable.jpg"
                },
                  new AuthorsDto()
                {
                    Name = "David Godman",
                    Biography = "David Godman's biography here",
                    ImageUrl =  "/images/authors/imagesunavailable.jpg"
                }
            };

            Genres = new List<GenresDto>()
            {
                new GenresDto()
                {
                    GenreName ="Comedy",
                    ImageUrl = "/images/genres/genreimageunavailable.jpg"
                },
                 new GenresDto()
                {
                    GenreName ="Trajedy",
                    ImageUrl = "/images/genres/genreimageunavailable.jpg"
                },
                 new GenresDto()
                {
                    GenreName ="Theatre",
                    ImageUrl = "/images/genres/genreimageunavailable.jpg"
                }
            };
        }
    }
}
