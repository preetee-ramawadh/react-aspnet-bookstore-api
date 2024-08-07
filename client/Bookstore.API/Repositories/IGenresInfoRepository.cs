using Bookstore.API.Entities;

namespace Bookstore.API.Repositories
{
    public interface IGenresInfoRepository
    {
        public Task<IEnumerable<Genres>> GetGenresAsync();

        public Task<Genres?> GetGenreAsync(int genreId);

        public Task<bool> GenreExistsAsync(string genreName);

        public Task AddGenreAsync(Genres genre);

        public Task<bool> SaveChangesAsync();

    }
}
