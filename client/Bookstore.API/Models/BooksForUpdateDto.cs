namespace Bookstore.API.Models
{
    public class BooksForUpdateDto
    {
        public required string Title { get; set; }

        public required int AuthorId { get; set; }

        public required int GenreId { get; set; }

        public required decimal Price { get; set; }

        public required DateOnly PublicationDate { get; set; }

       // public string? ImageUrl { get; set; }
    }

}
