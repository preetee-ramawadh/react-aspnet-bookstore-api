using AutoMapper;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Bookstore.API.Repositories;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;

        private readonly IGenresInfoRepository _genresInfoRepository;

        private readonly IMapper _mapper;
       

        public GenresController(ILogger<GenresController> logger, IGenresInfoRepository genresInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _genresInfoRepository = genresInfoRepository ?? throw new ArgumentNullException(nameof(_genresInfoRepository));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresDto>>> GetGenres()
        {
            var genres = await _genresInfoRepository.GetGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenresDto>>(genres));
           
        }

        [HttpGet("{id}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genresInfoRepository.GetGenreAsync(id);

            if(genre == null)
            {
                _logger.LogInformation($"Genre with id {id} wasn't found when accessing genre.");
               
                return NotFound();
            }

            return Ok(_mapper.Map<GenresDto>(genre));

        }

        [HttpPost]
        public async Task<ActionResult<GenresDto>> CreateGenre(GenresForCreationDto genreDto)
        {
            // Check if the genre exists
            if (await _genresInfoRepository.GenreExistsAsync(genreDto.GenreName))
            {
                return Conflict("Genre already exists.");  // Return 409 Conflict if the genre already exists
            }

            var genre = _mapper.Map<Entities.Genres>(genreDto);

            await _genresInfoRepository.AddGenreAsync(genre);

            await _genresInfoRepository.SaveChangesAsync();

            var createdGenreToReturn =
                _mapper.Map<GenresForCreationDto>(genre);

            // Return a CreatedAtRoute response with the location of the new resource
            return CreatedAtRoute("GetGenre", new { id = genre.GenreId }, createdGenreToReturn);

        }

    }
}
