namespace Bookstore.API.Models
{
    public class GenresDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }

        // List of books written in the Genre
        public IEnumerable<BooksDto>? Books { get; set; }
    }
}
    