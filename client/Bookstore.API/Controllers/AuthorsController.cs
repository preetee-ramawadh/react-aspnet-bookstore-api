
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;

        private readonly BookstoreDataStore _bookstoreDataStore;

        public AuthorsController(ILogger<AuthorsController> logger,
            BookstoreDataStore booksDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookstoreDataStore = booksDataStore ?? throw new ArgumentNullException(nameof(booksDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorsDto>> GetAuthors()
        {
            return Ok(_bookstoreDataStore.Authors);

        }

        [HttpGet("{id}")]
        public ActionResult<AuthorsDto> GetAuthorById(int id)
        {
            var authorToReturn = _bookstoreDataStore.Authors
                 .FirstOrDefault(author => author.Id == id);

            if (authorToReturn == null)
            {
                return NotFound();
            }

            return Ok(authorToReturn);

        }

        [HttpPost]
        public ActionResult<AuthorsDto> CreateAuthor(AuthorsForCreationDto author)
        {
            var authorToCreate = new AuthorsDto()
            {
                Name = author.Name,
                Biography = author.Biography,
                ImageUrl = author.ImageUrl,
            };

            return CreatedAtRoute("GetAuthorById", authorToCreate);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id, AuthorsForUpdateDto author)
        {
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateAuthor(int id, JsonPatchDocument<AuthorsForUpdateDto> patchDocument)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteAuthor(int id) 
        {
            var authorToDelete = _bookstoreDataStore.Authors
                .FirstOrDefault(author => author.Id == id);

            if (authorToDelete == null)
            {
                return NotFound();
            }
            //logic for author bound to book here

            _bookstoreDataStore.Authors.Remove(authorToDelete);

            return NoContent(); 
        }


    }
}
