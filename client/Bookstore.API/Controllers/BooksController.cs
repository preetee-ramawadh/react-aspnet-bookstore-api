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
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBooksInfoRepository _booksInfoRepository;

        private readonly IMapper _mapper;

        private readonly IStringLocalizer<Messages> _localizer;

        public BooksController(ILogger<BooksController> logger, IBooksInfoRepository booksInfoRepository, IMapper mapper, IStringLocalizer<Messages> localizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _booksInfoRepository = booksInfoRepository ?? throw new ArgumentNullException(nameof(booksInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDto>>> GetBooks()
        {
            var bookEntities = await _booksInfoRepository.GetBooksAsync();
            return Ok(_mapper.Map<IEnumerable<BooksDto>>(bookEntities));
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BooksDto>> GetBook(int id)
        {
            var book = await _booksInfoRepository.GetBookAsync(id);

            if (book == null)
            {
                var message = _localizer["BookNotFound"];
                //_logger.LogInformation($"Book with id {id} wasn't found when accessing Book.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });
            }

            return Ok(_mapper.Map<BooksDto>(book));

        }

        [HttpPost]
        public async Task<ActionResult<BooksDto>> CreateBook(BooksForCreationDto bookForCreation)
        {
            var book = _mapper.Map<Books>(bookForCreation);

            await _booksInfoRepository.AddBookAsync(book);

            await _booksInfoRepository.SaveChangesAsync();

            var createdBookToReturn =
                _mapper.Map<BooksDto>(book);

            return CreatedAtRoute("GetBook", new { id = createdBookToReturn.BookId }, createdBookToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BooksForUpdateDto bookForUpdateDto)
        {
            // Check if the book exists
            var existingBook = await _booksInfoRepository.GetBookAsync(id);
           
            if (existingBook == null)
            {
                var message = _localizer["BookNotFound"];
                _logger.LogInformation($"{message} Id: {id} ");
                return NotFound(new { message });  // Return 404 Not Found if the author entity is not found
            }

            // Map the updated properties from the DTO to the existing entity
            _mapper.Map(bookForUpdateDto, existingBook);

            //Save changes to the database
            await _booksInfoRepository.SaveChangesAsync();

            // Map the updated entity to DTO
            var updatedBookToReturn =
                _mapper.Map<BooksForUpdateDto>(existingBook);

            // Return an OK response with the updated data
            return Ok(updatedBookToReturn);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateBook(int id, JsonPatchDocument<BooksForUpdateDto> patchDocument)
        {
            // Check if the Book exists
            var existingBook = await _booksInfoRepository.GetBookAsync(id);

            if (existingBook == null)
            {
                var message = _localizer["BookNotFound"];
                // _logger.LogInformation($"Book with id {id} wasn't found when accessing Book.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });  // Return 404 Not Found if the book entity is not found
            }

            var bookToPatch = _mapper.Map<BooksForUpdateDto>(
                existingBook);

            patchDocument.ApplyTo(bookToPatch, ModelState);

            _mapper.Map(bookToPatch, existingBook);
            await _booksInfoRepository.SaveChangesAsync();

            // Indicate success with no content
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBook(int id)
        { 
            // Retrieve the Book entity
            var bookEntity = await _booksInfoRepository.GetBookAsync(id);

            // Check if the retrieved book entity is null
            if (bookEntity == null)
            {
                var message = _localizer["BookNotFound"];
                // _logger.LogInformation($"Book with id {id} wasn't found when accessing Book.");
                _logger.LogInformation($"{message} Id: {id} ");

                return NotFound(new { message });  // Return 404 Not Found if the book entity is not found
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
