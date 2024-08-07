using Bookstore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        private readonly BookstoreDataStore _bookstoreDataStore;

        public BooksController(ILogger<BooksController> logger,
            BookstoreDataStore booksDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookstoreDataStore = booksDataStore ?? throw new ArgumentNullException(nameof(booksDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<BooksDto>> GetBooks()
        {
            return Ok(_bookstoreDataStore.Books);

        }

        [HttpGet("{id}", Name = "GetBooksById")]
        public ActionResult<BooksDto> GetBookById(int id)
        {
            var bookToReturn = _bookstoreDataStore.Books
                 .FirstOrDefault(book => book.Id == id);

            if (bookToReturn == null)
            {
                return NotFound();
            }

            return Ok(bookToReturn);

        }

        [HttpPost]
        public ActionResult<BooksDto> CreateBook(BooksForCreationDto book)
        {
            var bookToCreate = new BooksDto()
            {
                Title = book.Title,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                Price = book.Price,
                PublicationDate = book.PublicationDate,
                ImageUrl = book.ImageUrl,
            };

            return CreatedAtRoute("GetBooksById", bookToCreate);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, BooksForUpdateDto book)
        {
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateBook(int id, JsonPatchDocument<BooksForUpdateDto> patchDocument)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteBook(int id)
        {
            var bookToDelete = _bookstoreDataStore.Books
                .FirstOrDefault(book => book.Id == id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            _bookstoreDataStore.Books.Remove(bookToDelete);

            return NoContent();
        }

    }
}
