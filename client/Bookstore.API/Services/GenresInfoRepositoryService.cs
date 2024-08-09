using Bookstore.API.DbContexts;
using Bookstore.API.Entities;
using Bookstore.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Services
{
    public class GenresInfoRepositoryService : IGenresInfoRepository
    {
        private readonly BookstoreInfoContext _bookstoreInfoContext;

        public GenresInfoRepositoryService(BookstoreInfoContext bookstoreInfoContex)
        {
            _bookstoreInfoContext = bookstoreInfoContex ?? throw new ArgumentNullException(nameof(bookstoreInfoContex));
        }
        
        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await _bookstoreInfoContext.Genres
                .Include(g => g.Books)
                .ToListAsync();
        }

        public async Task<Genres?> GetGenreAsync(int genreId)
        {
            return await _bookstoreInfoContext.Genres
                .Include(g => g.Books)
                .Where(g => g.GenreId == genreId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> GenreExistsAsync(string genreName)
        {
            var genreExists = 
                await _bookstoreInfoContext
                .Genres
                .AnyAsync(g => g.GenreName.ToLower() == genreName.ToLower());

            return genreExists;

        }

        public async Task AddGenreAsync(Genres genre)
        {
            // Check if the genre already exists
            bool genreNameExists = await GenreExistsAsync(genre.GenreName);

            if (genreNameExists)
            {
                throw new InvalidOperationException("Genre already exists.");
            }

            // Create a new Genre entity
            var newGenre = new Genres
            {
                GenreName = genre.GenreName
                // Initialize other properties if needed
            };

            // Add the new genre to the context
            await _bookstoreInfoContext.Genres.AddAsync(newGenre);

            // Save changes to the database
            await _bookstoreInfoContext.SaveChangesAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _bookstoreInfoContext.SaveChangesAsync() >= 1);
        }
    }
}
