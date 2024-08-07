using Bookstore.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace Bookstore.API.DbContexts
{
    public class BookstoreContext : DbContext
    { 
        public DbSet<Authors> Authors { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public DbSet<Books> Books { get; set; }

        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}


    }
}
