using System.ComponentModel.DataAnnotations;

namespace Bookstore.API.Models
{
    public class AuthorsForUpdateDto
    {
        [Required(ErrorMessage = "You Should Provide a Name Value")]
        [MaxLength(80)]
        public required string Name { get; set; }
        public required string Biography { get; set; }
       // public required string ImageUrl { get; set; }
    }
}
