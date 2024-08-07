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
        public required string GenreName { get; set; }

        public string ImageUrl { get; set; } = "/images/genres/genreimageunavailable.jpg";

        //giving parameterized ctor to avoid empty values for required field
        //public Genres(string name)
        //{
        //    GenreName = name;
        //}
    }
}
