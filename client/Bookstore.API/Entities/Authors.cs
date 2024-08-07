using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.API.Entities
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId {  get; set; }

        [Required]
        [MaxLength(80)]
        public string Name {  get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Biography { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "/images/authors/imagesunavailable.jpg";

        // Parameterized constructor to ensure required fields are initialized
        public Authors(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        // Parameterless constructor is required for EF Core
        public Authors() { }
    }
}
