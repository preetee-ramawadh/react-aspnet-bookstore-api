using Bookstore.API.Entities;

namespace Bookstore.API.Repositories
{
    public interface IAuthorsInfoRepository
    {
        public Task<IEnumerable<Authors>> GetAuthorsAsync();

        public Task<Authors?> GetAuthorAsync(int authorId);

        public Task<bool> AuthorExistsAsync(int authorId);

        public Task AddAuthorAsync(Authors author);

        public Task UpdateAuthor(Authors author);

        public void DeleteAuthor(Authors author);

        public Task<bool> SaveChangesAsync();

        //public Task<Authors> GetAuthorWithBooksAsync(int authorId);
    }
}
