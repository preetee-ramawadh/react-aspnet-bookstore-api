﻿
using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Repositories;
using Bookstore.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Bookstore.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;

        private readonly IAuthorsInfoRepository _authorsInfoRepository;

        private readonly IMapper _mapper;

        private readonly IStringLocalizer<Messages> _localizer;

        public AuthorsController(ILogger<AuthorsController> logger, 
            IAuthorsInfoRepository authorsInfoRepository, IMapper mapper, IStringLocalizer<Messages> localizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authorsInfoRepository = authorsInfoRepository ?? throw new ArgumentNullException(nameof(authorsInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAuthors()
        {
            var authorEntities = await _authorsInfoRepository.GetAuthorsAsync();

            return Ok(_mapper.Map<IEnumerable<AuthorsDto>>(authorEntities));
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorsDto>> GetAuthor(int id)
        {
            var author = await _authorsInfoRepository.GetAuthorAsync(id);

            if (author == null)
            {
                var message = _localizer["AuthorNotFound"];
                // _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });
            }

            var authorToReturn = _mapper.Map<AuthorsDto>(author);

            return Ok(authorToReturn);

        }

        [HttpPost]
        public async Task<ActionResult<AuthorsDto>> CreateAuthor(AuthorsForCreationDto authorForCreation)
        {
            var author = _mapper.Map<Authors>(authorForCreation);

            await _authorsInfoRepository.AddAuthorAsync(author);

            await _authorsInfoRepository.SaveChangesAsync();

            var createdAuthorToReturn =
                _mapper.Map<AuthorsDto>(author);

            return CreatedAtRoute
                (
                 "GetAuthor",
                 new { id = createdAuthorToReturn.AuthorId },
                 createdAuthorToReturn
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorsForUpdateDto authorForUpdateDto)
        {
            // Check if the Author exists
            var existingAuthor = await _authorsInfoRepository.GetAuthorAsync(id);

            if (existingAuthor == null)
            {
                var message = _localizer["AuthorNotFound"];
                // _logger.LogInformation($"Author with id {id} wasn't found when accessing Authors.");
                _logger.LogInformation($"{message} Id: {id} ");

                //return NotFound(new { message = "Author not found." });  // Return 404 Not Found if the author entity is not found
                return NotFound(new { message });
            }

            // Map the updated properties from the DTO to the existing entity
            _mapper.Map(authorForUpdateDto, existingAuthor);

            //Save changes to the database
            await _authorsInfoRepository.SaveChangesAsync();

            // Map the updated entity to DTO
            var updatedAuthorToReturn = _mapper.Map<AuthorsForUpdateDto>(existingAuthor);

            // Indicate success with no content
           // return NoContent();
            // return ok with updated data
            return Ok(updatedAuthorToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateAuthor(int id, JsonPatchDocument<AuthorsForUpdateDto> patchDocument)
        {
            // Check if the Author exists
            var existingAuthor = await _authorsInfoRepository.GetAuthorAsync(id);

            if (existingAuthor == null)
            { 
                var message = _localizer["AuthorNotFound"];

                // _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });  // Return 404 Not Found if the author entity is not found
            }

            var authorToPatch = _mapper.Map<AuthorsForUpdateDto>(
                existingAuthor);

            patchDocument.ApplyTo(authorToPatch, ModelState);

            _mapper.Map(authorToPatch, existingAuthor);
            await _authorsInfoRepository.SaveChangesAsync();

            // Indicate success with no content
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAuthor(int id) 
        {
            // Retrieve the Author entity
            var authorEntity = await _authorsInfoRepository.GetAuthorAsync(id);

            // Check if the retrieved author entity is null
            if (authorEntity == null)
            {
                var message = _localizer["AuthorNotFound"];

                // _logger.LogInformation($"Author with id {id} wasn't found when accessing Author.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });  // Return 404 Not Found if the author entity is not found
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
