namespace Bookstore.API.Models
{
    public class GenresDto
    {
        public int GenreId { get; set; }
        public required string GenreName { get; set; }
        public string ImageUrl { get; set; } = "/images/genres/genreimageunavailable.jpg";

        // List of books written in the Genre
        public ICollection<BooksTitleDto> Books { get; set; } = new List<BooksTitleDto>();


        //public IEnumerable<BooksDto>? Books { get; set; }
    }
}
    