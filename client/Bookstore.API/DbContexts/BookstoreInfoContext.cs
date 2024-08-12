using Bookstore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.DbContexts
{
    public class BookstoreInfoContext : DbContext
    {
        public DbSet<Authors> Authors { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genres>()
                 .HasData(
                new Genres()
                {
                    GenreId = 9,
                    GenreName = "Tragedy",
                    ImageUrl = "/images/genres/genreimageunavailable.jpg"
                },
                new Genres()
                {
                    GenreId = 10,
                    GenreName = "Theatre",
                    ImageUrl = "/images/genres/genreimageunavailable.jpg"
                });

            modelBuilder.Entity<Authors>()
                .HasData(
                new Authors()
                {
                    AuthorId = 9,
                    Name = "Author 2024",
                    Biography = "biography to be written"
                },
                 new Authors()
                 {
                     AuthorId = 16,
                     Name = "Author 2025",
                     Biography = "coming soon"
                 });

           modelBuilder.Entity<Books>()
                .HasData(
                new Books()
                {
                    BookId = 6,
                    Title = "Book title",
                    AuthorId = 4,
                    GenreId = 7,
                    Price = 899.99M,
                    PublicationDate = new DateOnly(2008, 1, 1)
                },
                 new Books()
                 {
                     BookId = 7,
                     Title = "Book Title2",
                     AuthorId = 5,
                     GenreId = 3,
                     Price = 99.99M,
                     PublicationDate = new DateOnly(2022, 1, 1)
                 });
        }
        public BookstoreInfoContext(DbContextOptions<BookstoreInfoContext> options) : base(options)
        {
        }

    }
}
