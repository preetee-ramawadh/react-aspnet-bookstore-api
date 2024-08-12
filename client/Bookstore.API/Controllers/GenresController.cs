using AutoMapper;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Bookstore.API.Repositories;
using Microsoft.Extensions.Localization;
using Bookstore.API.Resources;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;

        private readonly IGenresInfoRepository _genresInfoRepository;

        private readonly IMapper _mapper;

        private readonly IStringLocalizer<Messages> _localizer;
       

        public GenresController(ILogger<GenresController> logger, IGenresInfoRepository genresInfoRepository, IMapper mapper, IStringLocalizer<Messages> localizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _genresInfoRepository = genresInfoRepository ?? throw new ArgumentNullException(nameof(_genresInfoRepository));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresDto>>> GetGenres()
        {
            var genres = await _genresInfoRepository.GetGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenresDto>>(genres));
           
        }

        [HttpGet("{id}", Name = "GetGenre")]
        public async Task<ActionResult<GenresDto>> GetGenre(int id)
        {
            var genre = await _genresInfoRepository.GetGenreAsync(id);

            if(genre == null)
            {
                var message = _localizer["GenreNotFound"];
               // _logger.LogInformation($"Genre with id {id} wasn't found when accessing genre.");
                _logger.LogInformation($"{ message } Id: {id} ");

                return NotFound(new { message });
            }

            var genreToReturn = _mapper.Map<GenresDto>(genre);

            return Ok(genreToReturn);

        }

        [HttpPost]
        public async Task<ActionResult<GenresDto>> CreateGenre(GenresForCreationDto genreForCreation)
        {
            // Check if the genre exists
            var genreExists = 
                await _genresInfoRepository.GenreExistsAsync(genreForCreation.GenreName);

            if (genreExists)
            {
                return Conflict("Genre already exists.");  // Return 409 Conflict if the genre already exists
            }

            var genre = _mapper.Map<Entities.Genres>(genreForCreation);

            await _genresInfoRepository.AddGenreAsync(genre);

            await _genresInfoRepository.SaveChangesAsync();

            var createdGenreToReturn =
                _mapper.Map<GenresDto>(genre);

            // Return a CreatedAtRoute response with the location of the new resource
            return CreatedAtRoute("GetGenre", new { id = createdGenreToReturn.GenreId }, createdGenreToReturn);

        }

    }
}
