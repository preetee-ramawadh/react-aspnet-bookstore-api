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
        [MaxLength(1000)]
        public string Biography { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = "/images/authors/imagesunavailable.jpg";

        public virtual ICollection<Books> Books { get; set; } = new List<Books>();// Navigation property
    }

    // Parameterized constructor to ensure required fields are initialized
    //public Authors(string name, string biography, string imageUrl)
    //{
    //    Name = name;
    //    Biography = biography;
    //    ImageUrl = imageUrl;
    //}

    //// Parameterless constructor is required for EF Core
    //public Authors() { }
    //}
}
