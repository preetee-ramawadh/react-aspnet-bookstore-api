using Bookstore.API.Entities;
using Bookstore.API.DbContexts;
using Bookstore.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Services
{
    public class AuthorsInfoRepositoryService : IAuthorsInfoRepository
    {
        private readonly BookstoreInfoContext _bookstoreInfoContext;

        public AuthorsInfoRepositoryService(BookstoreInfoContext bookstoreInfoContext)
        {
            _bookstoreInfoContext = bookstoreInfoContext ?? throw new ArgumentNullException(nameof(bookstoreInfoContext));
        }

        public async Task<IEnumerable<Authors>> GetAuthorsAsync()
        {
            return await _bookstoreInfoContext.Authors.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Authors?> GetAuthorAsync(int authorId)
        {
            return await _bookstoreInfoContext.Authors
                 .Where(a => a.AuthorId == authorId).FirstOrDefaultAsync();
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _bookstoreInfoContext.Authors.AnyAsync(a => a.AuthorId == authorId);
        }

        public async Task AddAuthorAsync(Authors author)
        {
            // Create a new Author entity
            var newAuthor = new Authors
            {
                Name = author.Name,
                Biography = author.Biography,
                // Initialize other properties if needed
            };

            // Add the new genre to the context
            await _bookstoreInfoContext.Authors.AddAsync(newAuthor);

            // Save changes to the database
            await _bookstoreInfoContext.SaveChangesAsync();
        }

        public async Task UpdateAuthor(Authors author)
        {
            _bookstoreInfoContext.Authors.Update(author);
        }

        public void DeleteAuthor(Authors author)
        {
            _bookstoreInfoContext.Authors.Remove(author);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _bookstoreInfoContext.SaveChangesAsync() >= 1);
        }
    }
}
