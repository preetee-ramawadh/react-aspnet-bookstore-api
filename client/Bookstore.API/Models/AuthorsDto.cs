namespace Bookstore.API.Models
{
    public class AuthorsDto
    {

        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Biography { get; set; }
        public required string ImageUrl { get; set; }

        // List of books written by the author
        public IEnumerable<BooksDto>? Books { get; set; }

    }
}
