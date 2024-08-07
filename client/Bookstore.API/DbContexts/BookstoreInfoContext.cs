using Bookstore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.DbContexts
{
    public class BookstoreInfoContext : DbContext
    {
        public DbSet<Authors> Authors { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public DbSet<Books> Books { get; set; }


        public BookstoreInfoContext(DbContextOptions<BookstoreInfoContext> options) : base(options)
        {
        }

    }
}
