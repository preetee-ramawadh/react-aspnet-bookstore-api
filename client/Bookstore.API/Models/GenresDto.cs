namespace Bookstore.API.Models
{
    public class GenresDto
    {
        public int GenreId { get; set; }
        public required string GenreName { get; set; }
        public required string ImageUrl { get; set; }

        // List of books written in the Genre
        public ICollection<BooksTitleDto> Books { get; set; } = new List<BooksTitleDto>();


        //public IEnumerable<BooksDto>? Books { get; set; }
    }
}
    