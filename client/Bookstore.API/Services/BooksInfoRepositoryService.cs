using Bookstore.API.DbContexts;
using Bookstore.API.Entities;
using Bookstore.API.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public Task AddBookAsync(Books book)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBook(Books book)
        {
            _bookstoreInfoContext.Books.Update(book);
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
