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

        [MaxLength(100)]
        public string ImageUrl { get; set; } = "/images/genres/genreimageunavailable.jpg";

        public virtual ICollection<Books> Books { get; set; } = new List<Books>();// Navigation property

        // Parameterized constructor to ensure required fields are initialized
        public Genres(string name)
        {
            GenreName = name;
            Books = new List<Books>();
        }

        public Genres(){}
    }
}
