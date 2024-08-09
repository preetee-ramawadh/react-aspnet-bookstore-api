using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.API.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]  // Specifies the precision and scale
        public decimal Price { get; set; }

        [Required]
        public DateOnly PublicationDate { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; } = "/images/books/imageunavailable.jpg";

        [ForeignKey(nameof(AuthorId))]
        public Authors? Author { get; set; }  // Navigation property

        [ForeignKey(nameof(GenreId))]
        public Genres? Genre { get; set; }

        // Parameterized constructor to initialize required fields
        public Books(string title, int authorId, int genreId, decimal price, DateOnly publicationDate)
        {
            Title = title;
            AuthorId = authorId;
            GenreId = genreId;
            Price = price;
            PublicationDate = publicationDate;
        }

        // Parameterless constructor is required for EF Core
        public Books() { }
    }
}
