using Bookstore.API.DbContexts;
using Bookstore.API.Entities;
using Bookstore.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Bookstore.API.Services
{
    public class BooksInfoRepositoryService : IBooksInfoRepository
    {
        private readonly BookstoreInfoContext _bookstoreInfoContext;

        public BooksInfoRepositoryService(BookstoreInfoContext bookstoreInfoContext)
        {
            _bookstoreInfoContext = bookstoreInfoContext ?? throw new ArgumentNullException(nameof(bookstoreInfoContext));
        }

        public async Task<IEnumerable<Books>> GetBooksAsync()
        {
            return await _bookstoreInfoContext.Books
                .Include(b => b.Author) // Eagerly load Author
                .Include(b => b.Genre).  // Eagerly load Genre
                ToListAsync();
        }

        public async  Task<Books?> GetBookAsync(int bookId)
        {
            return await _bookstoreInfoContext.Books
                .Include(b => b.Author) // Eagerly load Author
                .Include(b => b.Genre)  // Eagerly load Genre.
                .Where(b => b.BookId == bookId).FirstOrDefaultAsync();
        }
        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _bookstoreInfoContext.Books.AnyAsync(b => b.BookId == bookId);
        }

        public async Task AddBookAsync(Books book)
        {
            // Create a new Book entity
            var newBook = new Books
            {
               Title = book.Title,
               AuthorId = book.AuthorId,
               GenreId = book.GenreId,
               Price = book.Price,
               PublicationDate = book.PublicationDate
            };

            // Add the new Book to the database context
            await _bookstoreInfoContext.Books.AddAsync(newBook);

            // Save changes to the database
            await _bookstoreInfoContext.SaveChangesAsync();

        }

        public async Task UpdateBook(Books book)
        {
            _bookstoreInfoContext.Books.Update(book);

            // Save changes to the database
            await _bookstoreInfoContext.SaveChangesAsync();
        }

        public void DeleteBook(Books book)
        {
            _bookstoreInfoContext.Books.Remove(book);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _bookstoreInfoContext.SaveChangesAsync() >= 1);
        }
    }
}
