namespace Bookstore.API.Models
{
    public class BooksDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required int AuthorId { get; set; }

        public required int GenreId { get; set; }

        public required decimal Price { get; set; }

        public required DateOnly PublicationDate { get; set; }

        public required string ImageUrl { get; set; }

        // Including Author's name
        public AuthorsDto? Author { get; set; }

        public GenresDto? Genre { get; set; }
    }
}
