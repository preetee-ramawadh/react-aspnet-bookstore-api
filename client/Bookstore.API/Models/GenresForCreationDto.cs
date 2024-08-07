using System.ComponentModel.DataAnnotations;

namespace Bookstore.API.Models
{
    public class GenresForCreationDto
    {
        [Required(ErrorMessage = "You Should Provide a Name Value")]
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
    }
}
