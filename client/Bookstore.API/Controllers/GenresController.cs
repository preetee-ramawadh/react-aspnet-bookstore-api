using Bookstore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookstore.API.Entities;
using System;
using Bookstore.API.DbContexts;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;

        private readonly BookstoreDataStore _bookstoreDataStore;

        private readonly BookstoreContext _context;

        public GenresController(ILogger<GenresController> logger,
            BookstoreDataStore booksDataStore, BookstoreContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookstoreDataStore = booksDataStore ?? throw new ArgumentNullException(nameof(booksDataStore));
            _context = context ?? throw new ArgumentNullException(nameof(booksDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<GenresDto>> getGenres()
        {
            return Ok(_bookstoreDataStore.Genres);
           
        }

        [HttpGet("{id}")]
        public ActionResult<GenresDto> GetGenreById(int id)
        {
            var genreToReturn = "";

            if(genreToReturn == null)
            {
                return NotFound();
            }

            return Ok(genreToReturn);

        }

        [HttpPost]
        public ActionResult CreateAuthor(GenresForCreationDto genreDto)
        {
            // Check if the genre already exists
            var existingGenre =  _bookstoreDataStore.Genres
                .FirstOrDefault(g => g.Name == genreDto.Name);

            if (existingGenre != null)
            {
                return Conflict("Genre already exists.");  // Return 409 Conflict if the genre already exists
            }

            // Create a new genre entity
            var genreToCreate = new Genres()
            {
                GenreName = genreDto.Name,
                ImageUrl = genreDto.ImageUrl,
            };

            // Add the new genre to the database
            _context.Genres.Add(genreToCreate);


            // Map the created genre to DTO
            var createdGenreDto = new GenresDto
            {
                Id = genreToCreate.GenreId,  // Assuming you have an Id property
                Name = genreToCreate.GenreName,
                ImageUrl = genreToCreate.ImageUrl,
            };

            // Return a CreatedAtRoute response with the location of the new resource
            return CreatedAtRoute(
                "getGenreById",
                new { id = createdGenreDto.Id },
                createdGenreDto
            );

        }

    }
}
