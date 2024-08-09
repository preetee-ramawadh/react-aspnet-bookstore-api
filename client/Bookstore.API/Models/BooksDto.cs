namespace Bookstore.API.Models
{
    public class BooksDto
    {
        public int BookId { get; set; }

        public required string Title { get; set; }

        public required int AuthorId { get; set; }

        public required int GenreId { get; set; }

        public required decimal Price { get; set; }

        public required DateOnly PublicationDate { get; set; }

        public string ImageUrl { get; set; } = "/images/books/imageunavailable.jpg";

        public AuthorsNameDto? Author { get; set; }

        public GenresNameDto? Genre { get; set; }

    }
}
