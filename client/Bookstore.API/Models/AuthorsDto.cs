namespace Bookstore.API.Models
{
    public class AuthorsDto
    {

        public int AuthorId { get; set; }
        public required string Name { get; set; }

        public required string Biography { get; set; }
        public string ImageUrl { get; set; } = "/images/authors/imagesunavailable.jpg";

        // List of books written by the author
        public ICollection<BooksTitleDto> Books { get; set; } = new List<BooksTitleDto>();

    }
}
