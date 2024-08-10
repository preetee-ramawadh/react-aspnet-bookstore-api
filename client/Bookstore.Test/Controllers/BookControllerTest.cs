using AutoMapper;
using Bookstore.API.Controllers;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Bookstore.Test.Controllers
{
    public class BookControllerTest
    {
        private readonly BooksController _booksController;
        private readonly IBooksInfoRepository fakeRepository;
        private readonly IMapper fakeMapper;
        private readonly ILogger<BooksController> fakeLogger;
        public BookControllerTest()
        {
            //set up Dependencies
            fakeRepository = A.Fake<IBooksInfoRepository>();
            fakeMapper = A.Fake<IMapper>();
            fakeLogger = A.Fake<ILogger<BooksController>>();

            //SUT --> System under Test
            _booksController = new BooksController(fakeLogger, fakeRepository, fakeMapper);

        }

        //[HttpGet]
        [Fact]
        public async Task GetBooks_ShouldReturnOk_WithBooks_WhenBooksExist()
        {
            // Arrange
           
            var bookEntities = new List<Books>
        {
            new Books { BookId = 1, Title = "Book 1", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1)},
            new Books { BookId = 2, Title = "Book 2", AuthorId = 3, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2024, 1, 1) }
        };

            var bookDtos = new List<BooksDto>
        {
            new BooksDto { BookId = 1, Title = "Book 1", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) },
            new BooksDto { BookId = 2, Title = "Book 2", AuthorId = 3, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2024, 1, 1) }
        };

            A.CallTo(() => fakeRepository.GetBooksAsync()).Returns(Task.FromResult(bookEntities.AsEnumerable()));
            A.CallTo(() => fakeMapper.Map<IEnumerable<BooksDto>>(bookEntities)).Returns(bookDtos);
            // Act
            var result = await _booksController.GetBooks();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(bookDtos);
            //var returnedDtos = okResult.Value.Should().BeAssignableTo<IEnumerable<BooksDto>>().Subject;
            //returnedDtos.Should().HaveCount(2);
            //returnedDtos.Should().ContainEquivalentOf(bookDtos.First());
            //returnedDtos.Should().ContainEquivalentOf(bookDtos.Last());
        }

        [Fact]
        public async Task GetBooks_ShouldReturnOk_WithEmptyList_WhenNoBooksExist()
        {
            // Arrange
            var bookEntities = new List<Books>(); // No books
            var bookDtos = new List<BooksDto>(); // No DTOs

            A.CallTo(() => fakeRepository.GetBooksAsync()).Returns(Task.FromResult(bookEntities.AsEnumerable()));
            A.CallTo(() => fakeMapper.Map<IEnumerable<BooksDto>>(bookEntities)).Returns(bookDtos);

            // Act
            var result = await _booksController.GetBooks();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDtos = okResult.Value.Should().BeAssignableTo<IEnumerable<BooksDto>>().Subject;
            returnedDtos.Should().BeEmpty();
        }

        //[HttpGet("{id}")]
        [Fact]
        public async Task GetBook_ShouldReturnOk_WhenBookExists()
        {
            // Arrange

            var bookEntity = new Books { BookId = 1, Title = "Book 1", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };
            var bookDto = new BooksDto { BookId = 1, Title = "Sample Book", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2024, 1, 1) };

            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult(bookEntity));
            A.CallTo(() => fakeMapper.Map<BooksDto>(bookEntity)).Returns(bookDto);

            // Act
            var result = await _booksController.GetBook(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<BooksDto>().Subject;
            returnedDto.Should().BeEquivalentTo(bookDto);
        }

        [Fact]
        public async Task GetBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            
            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult<Books>(null));

            // Act
            var result = await _booksController.GetBook(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        // create
        [Fact]
        public async Task CreateBook_ShouldReturnCreatedAtRoute_WhenBookIsSuccessfullyCreated()
        {
            // Arrange
            
            var bookForCreation = new BooksForCreationDto { Title = "New Book", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };
            var bookEntity = new Books { BookId = 1, Title = "New Book", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };
            var createdBookDto = new BooksDto { BookId = 1, Title = "New Book", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };

            A.CallTo(() => fakeMapper.Map<Books>(bookForCreation)).Returns(bookEntity);
            A.CallTo(() => fakeRepository.AddBookAsync(bookEntity)).Returns(Task.CompletedTask);
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);
            A.CallTo(() => fakeMapper.Map<BooksDto>(bookEntity)).Returns(createdBookDto);

            // Act
            var result = await _booksController.CreateBook(bookForCreation);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdResult.RouteName.Should().Be("GetBook");
            createdResult.RouteValues["id"].Should().Be(1);
            var returnedDto = createdResult.Value.Should().BeOfType<BooksDto>().Subject;
            returnedDto.Should().BeEquivalentTo(createdBookDto);
        }

        //update
        [Fact]
        public async Task UpdateBook_ShouldReturnOk_WhenBookExists()
        {
            // Arrange
            
            var existingBook = new Books { BookId = 1, Title = "Old Title", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };
            var bookForUpdateDto = new BooksForUpdateDto { Title = "Updated Title", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };
            var updatedBook = new Books { BookId = 1, Title = "Updated Title", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };

            var updatedBookDto = new BooksForUpdateDto { Title = "Updated Title", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };

            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult(existingBook));
            A.CallTo(() => fakeMapper.Map(bookForUpdateDto, existingBook));
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);
            A.CallTo(() => fakeMapper.Map<BooksForUpdateDto>(existingBook)).Returns(updatedBookDto);

            // Act
            var result = await _booksController.UpdateBook(1, bookForUpdateDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<BooksForUpdateDto>().Subject;
            returnedDto.Should().BeEquivalentTo(updatedBookDto);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
           // BooksForUpdateDto bookForUpdateDto; //{ Title = "Updated Title", AuthorId = 2, GenreId = 3, Price = 20.00M, PublicationDate = new DateOnly(2023, 1, 1) };

            // Arrange
            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult<Books>(null));

            // Act
           // var result = await _booksController.UpdateBook(1, new BooksForUpdateDto());

            // Assert
          //  result.Should().BeOfType<NotFoundResult>();
        }

        //Delete
        [Fact]
        public async Task DeleteBook_ShouldReturnNoContent_WhenBookExists()
        {
            // Arrange
            var bookEntity = new Books { BookId = 1, Title = "Sample Book" };

            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult(bookEntity));
            A.CallTo(() => fakeRepository.DeleteBook(bookEntity)).DoesNothing();
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);

            // Act
            var result = await _booksController.DeleteBook(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult<Books>(null));

            // Act
            var result = await _booksController.DeleteBook(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        //Partial update
        [Fact]
        public async Task PartiallyUpdateBook_ShouldReturnNoContent_WhenBookExistsAndPatchIsValid()
        {
            // Arrange
            var existingBook = new Books { BookId = 1, Title = "Original Title", AuthorId = 1, GenreId = 1, Price = 10.0m, PublicationDate = new DateOnly(2024, 1, 1) };
            var bookToPatch = new BooksForUpdateDto { Title = "Updated Title", AuthorId = 1, GenreId = 1, Price = 20.0m, PublicationDate = new DateOnly(2024, 2, 1) };
            var updatedBook = new Books { BookId = 1, Title = "Updated Title", AuthorId = 1, GenreId = 1, Price = 20.0m, PublicationDate = new DateOnly(2024, 2, 1) };

            var patchDoc = new JsonPatchDocument<BooksForUpdateDto>();
            patchDoc.Replace(b => b.Title, "Updated Title");
            patchDoc.Replace(b => b.Price, 20.0m);

            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult(existingBook));
            A.CallTo(() => fakeMapper.Map<BooksForUpdateDto>(existingBook)).Returns(bookToPatch);
            A.CallTo(() => fakeMapper.Map(bookToPatch, existingBook));
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);

            // Act
            var result = await _booksController.PartiallyUpdateBook(1, patchDoc);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task PartiallyUpdateBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var patchDoc = new JsonPatchDocument<BooksForUpdateDto>();
            patchDoc.Replace(b => b.Title, "Updated Title");

            A.CallTo(() => fakeRepository.GetBookAsync(1)).Returns(Task.FromResult<Books>(null));

            // Act
            var result = await _booksController.PartiallyUpdateBook(1, patchDoc);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }


    }

    

}
