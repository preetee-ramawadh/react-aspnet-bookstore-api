
using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;

        private readonly IAuthorsInfoRepository _authorsInfoRepository;

        private readonly IMapper _mapper;

        public AuthorsController(ILogger<AuthorsController> logger, 
            IAuthorsInfoRepository authorsInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authorsInfoRepository = authorsInfoRepository ?? throw new ArgumentNullException(nameof(authorsInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAuthors()
        {
            var authorEntities = await _authorsInfoRepository.GetAuthorsAsync();

            return Ok(_mapper.Map<IEnumerable<AuthorsDto>>(authorEntities));
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorsInfoRepository.GetAuthorAsync(id);

            if (author == null)
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorsDto>(author));

        }

        [HttpPost]
        public ActionResult<AuthorsDto> CreateAuthor(AuthorsForCreationDto author)
        {
            var authorToCreate = new AuthorsDto()
            {
                Name = author.Name,
                Biography = author.Biography,
                //ImageUrl = author.ImageUrl,
            };

            return CreatedAtRoute("GetAuthor", authorToCreate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorsForUpdateDto authorForUpdateDto)
        {
            if(!await _authorsInfoRepository.AuthorExistsAsync(id))
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");

                return NotFound();
            }

            // Retrieve the existing author entity
            var existingAuthor = await _authorsInfoRepository.GetAuthorAsync(id);

            if (existingAuthor == null)
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");

                return NotFound();  // Return 404 Not Found if the author entity is not found
            }

            // Map the updated properties from the DTO to the existing entity
            _mapper.Map(authorForUpdateDto, existingAuthor);

            // Update the author entity in the repository
            _authorsInfoRepository.UpdateAuthor(existingAuthor);

            // Save changes to the database
            await _authorsInfoRepository.SaveChangesAsync();

            // Map the updated entity to DTO
            var updatedAuthorToReturn = _mapper.Map<AuthorsForUpdateDto>(existingAuthor);

            // Return Ok with the updated resource
            return Ok(updatedAuthorToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateAuthor(int id, JsonPatchDocument<AuthorsForUpdateDto> patchDocument)
        {
            // Check if the author exists
            if (!await _authorsInfoRepository.AuthorExistsAsync(id))
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");

                return NotFound();
            }
            // Retrieve the existing author entity
            var existingAuthor = await _authorsInfoRepository.GetAuthorAsync(id);

            if (existingAuthor == null)
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");

                return NotFound();  // Return 404 Not Found if the author entity is not found
            }

            var authorToPatch = _mapper.Map<AuthorsForUpdateDto>(
                existingAuthor);

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAuthor(int id) 
        {
            // Check if the author exists
            if (!await _authorsInfoRepository.AuthorExistsAsync(id))
            {
                _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");

                return NotFound();
            }

            // Retrieve the author entity
            var authorEntity = await _authorsInfoRepository.GetAuthorAsync(id);

            // Check if the retrieved author entity is null
            if (authorEntity == null)
            {
                return NotFound();  // Return 404 Not Found if the author entity is not found
            }

            // Delete the author entity
            _authorsInfoRepository.DeleteAuthor(authorEntity);

            // Save changes to the database
            await _authorsInfoRepository.SaveChangesAsync();

            // Return NoContent status to indicate successful deletion
            return NoContent();
        }


    }
}
