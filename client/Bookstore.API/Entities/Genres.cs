using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.API.Entities
{
    public class Genres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "/images/genres/genreimageunavailable.jpg";

        // Parameterized constructor to ensure required fields are initialized
        public Genres(string name)
        {
            GenreName = name;
        }

        public Genres(){}
    }
}
