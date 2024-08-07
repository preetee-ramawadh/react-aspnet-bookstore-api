using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBooksInfoRepository _booksInfoRepository;

        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBooksInfoRepository booksInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _booksInfoRepository = booksInfoRepository ?? throw new ArgumentNullException(nameof(booksInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDto>>> GetBooks()
        {
            var bookEntities = await _booksInfoRepository.GetBooksAsync();
            return Ok(_mapper.Map<IEnumerable<BooksDto>>(bookEntities));
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _booksInfoRepository.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BooksDto>(book));

        }

        [HttpPost]
        public async Task<ActionResult<BooksDto>> CreateBook(BooksForCreationDto book)
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

            return CreatedAtRoute("GetBook", bookToCreate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BooksForUpdateDto bookDto)
        {
            if (!await _booksInfoRepository.BookExistsAsync(id))
            {
                return NotFound();
            }
            var bookEntity = _mapper.Map<Books>(bookDto);

            // Check if the retrieved author entity is null
            if (bookEntity == null)
            {
                return NotFound();  // Return 404 Not Found if the author entity is not found
            }

            await _booksInfoRepository.GetBookAsync(bookEntity.BookId);

            await _booksInfoRepository.SaveChangesAsync();
            var updatedBookToReturn =
                _mapper.Map<BooksForUpdateDto>(bookEntity);

            // Return a CreatedAtRoute response with the location of the new resource
            return CreatedAtRoute("GetBook", new { id = bookEntity.BookId }, updatedBookToReturn);
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateBook(int id, JsonPatchDocument<BooksForUpdateDto> patchDocument)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBook(int id)
        {
            // Check if the book exists
            if (!await _booksInfoRepository.BookExistsAsync(id))
            {
                return NotFound();
            }

            // Retrieve the book entity
            var bookEntity = await _booksInfoRepository.GetBookAsync(id);

            // Check if the retrieved book entity is null
            if (bookEntity == null)
            {
                return NotFound();  // Return 404 Not Found if the book entity is not found
            }

            // Delete the book entity
            _booksInfoRepository.DeleteBook(bookEntity);

            // Save changes to the database
            await _booksInfoRepository.SaveChangesAsync();

            // Return NoContent status to indicate successful deletion
            return NoContent();
        }

    }
}
