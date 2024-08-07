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
        public string Name {  get; set; }

        [Required]
        [MaxLength(200)]
        public string Biography { get; set; }

        public string ImageUrl { get; set; } = "/images/authors/imagesunavailable.jpg";
       
        //giving parameterized ctor to avoid empty values for required fields
        public Authors(string name, string biography ) {
            Name = name;
            Biography = biography;
        }
    }
}
