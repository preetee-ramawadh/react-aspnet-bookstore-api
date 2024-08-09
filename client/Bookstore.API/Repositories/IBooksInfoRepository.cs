using Bookstore.API.Entities;

namespace Bookstore.API.Repositories
{
    public interface IBooksInfoRepository
    {
        public Task<IEnumerable<Books>> GetBooksAsync();

        public Task<Books?> GetBookAsync(int bookId);

        public Task<bool> BookExistsAsync(int bookId);

        public Task AddBookAsync(Books book);

        public Task UpdateBook(Books book);

        public void DeleteBook(Books book);

        public Task<bool> SaveChangesAsync();
    }
}
