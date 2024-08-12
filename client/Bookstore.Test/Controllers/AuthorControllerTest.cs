using AutoMapper;
using Bookstore.API.Controllers;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Repositories;
using Bookstore.API.Resources;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Bookstore.Test.Controllers
{
    public class AuthorControllerTest
    {
        private readonly AuthorsController _authorsController;
        private readonly IAuthorsInfoRepository fakeRepository;
        private readonly IMapper fakeMapper;
        private readonly ILogger<AuthorsController> fakeLogger;
        private readonly IStringLocalizer<Messages> fakeLocalizer;

        public AuthorControllerTest()
        {
            //set up Dependencies
            fakeRepository = A.Fake<IAuthorsInfoRepository>();
            fakeMapper = A.Fake<IMapper>();
            fakeLogger = A.Fake<ILogger<AuthorsController>>();
            fakeLocalizer = A.Fake<IStringLocalizer<Messages>>();

            //SUT --> System under Test
            _authorsController = new AuthorsController(fakeLogger, fakeRepository, fakeMapper, fakeLocalizer);
        }

        // [HttpGet]
        [Fact]
        public async Task GetAuthors_ShouldReturnOk_WhenAuthorsAreAvailable()
        {
            // Arrange
            var authorEntities = new List<Authors>
        {
            new Authors { AuthorId = 1, Name = "Author One", Biography = "Bio One", ImageUrl = "/images/authors/author1.jpg" },
            new Authors { AuthorId = 2, Name = "Author Two", Biography = "Bio Two", ImageUrl = "/images/authors/author2.jpg" }
        };

            var authorsDto = new List<AuthorsDto>
        {
            new AuthorsDto { Name = "Author One", Biography = "author with id 1 biography" },
            new AuthorsDto { Name = "Author Two",  Biography = "author with id 2 biography" }
        };

            A.CallTo(() => fakeRepository.GetAuthorsAsync()).Returns(Task.FromResult<IEnumerable<Authors>>(authorEntities));
            A.CallTo(() => fakeMapper.Map<IEnumerable<AuthorsDto>>(authorEntities)).Returns(authorsDto);

            // Act
            var result = await _authorsController.GetAuthors();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(authorsDto);
        }

        // [HttpGet]
        [Fact]
        public async Task GetAuthors_ShouldReturnOk_WhenNoAuthorsAreAvailable()
        {
            // Arrange
            var authorEntities = new List<Authors>();
            var authorsDto = new List<AuthorsDto>();

            A.CallTo(() => fakeRepository.GetAuthorsAsync()).Returns(Task.FromResult<IEnumerable<Authors>>(authorEntities));
            A.CallTo(() => fakeMapper.Map<IEnumerable<AuthorsDto>>(authorEntities)).Returns(authorsDto);

            // Act
            var result = await _authorsController.GetAuthors();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(authorsDto);
        }

        // [HttpGet("{id}", Name = "GetAuthor")]
        [Fact]
        public async Task GetAuthor_ShouldReturnNotFound_WhenAuthorDoesNotExist()
        {
           
            A.CallTo(() => fakeRepository.GetAuthorAsync(A<int>.Ignored)).Returns(Task.FromResult<Authors>(null));
            // Act
            var result = await _authorsController.GetAuthor(1);

            // Assert
            //result.Result.Should().BeOfType<NotFoundResult>();
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        // [HttpGet("{id}", Name = "GetAuthor")]
        [Fact]
        public async Task GetAuthor_ShouldReturnOk_WhenAuthorExists()
        {
            // Arrange
            var author = new Authors { AuthorId = 1, Name = "Test Author" };
            var authorDto = new AuthorsDto { AuthorId = 1, Name = "Test Author", Biography = "Test Author Bio" };
            A.CallTo(() => fakeRepository.GetAuthorAsync(1)).Returns(Task.FromResult(author));
            A.CallTo(() => fakeMapper.Map<AuthorsDto>(author)).Returns(authorDto);

            // Act
            var result = await _authorsController.GetAuthor(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<AuthorsDto>().Subject;
            returnedDto.Name.Should().Be("Test Author");
        }

        // [HttpPost]
        [Fact]
        public async Task CreateAuthor_ShouldReturnCreatedAtRoute_WhenAuthorIsSuccessfullyCreated()
        {
            // Arrange
           
            var authorForCreation = new AuthorsForCreationDto { Name = "New Author", Biography = "Biography" };
            var author = new Authors { AuthorId = 1, Name = "New Author" };
            var authorDto = new AuthorsDto { AuthorId = 1, Name = "New Author", Biography = "Biography" };

            A.CallTo(() => fakeMapper.Map<Authors>(authorForCreation)).Returns(author);
            A.CallTo(() => fakeRepository.AddAuthorAsync(author)).Returns(Task.CompletedTask);
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);
            A.CallTo(() => fakeMapper.Map<AuthorsDto>(author)).Returns(authorDto);

            // Act
            var result = await _authorsController.CreateAuthor(authorForCreation);

            // Assert
            result.Result.Should().BeOfType<CreatedAtRouteResult>("because the action should return a CreatedAtRouteResult on successful creation");

            var createdResult = (CreatedAtRouteResult)result.Result;
            createdResult.RouteName.Should().Be("GetAuthor", "because the route name should be 'GetAuthor'");
            createdResult.RouteValues["id"].Should().Be(authorDto.AuthorId, "because the route values should include the correct author ID");
            var returnedDto = createdResult.Value.Should().BeOfType<AuthorsDto>("because the result value should be an instance of AuthorsDto").Subject;
            returnedDto.Name.Should().Be("New Author", "because the returned DTO should have the correct name");

        }

        //  [HttpDelete("{id}")]
        [Fact]
        public async Task DeleteAuthor_ShouldReturnNoContent_WhenAuthorExists()
        {
            // Arrange

            var author = new Authors { AuthorId = 1, Name = "Existing Author", Biography = "bio" };

            A.CallTo(() => fakeRepository.GetAuthorAsync(1)).Returns(Task.FromResult(author));
            A.CallTo(() => fakeRepository.DeleteAuthor(author)).DoesNothing();
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);

            // Act
            var result = await _authorsController.DeleteAuthor(1);

            // Assert
            result.Should().BeOfType<NoContentResult>("because the author exists and should be successfully deleted");
        }

        // [HttpPut("{id}")]
        [Fact]
        public async Task UpdateAuthor_ShouldReturnOk_WhenAuthorExists()
        {
            // Arrange
           
            var existingAuthor = new Authors { AuthorId = 1, Name = "Old Name", Biography = "bio" };
            var authorForUpdateDto = new AuthorsForUpdateDto { Name = "Updated Name", Biography = "bio" };
            var updatedAuthorDto = new AuthorsForUpdateDto { Name = "Updated Name", Biography = "bio" };

            A.CallTo(() => fakeRepository.GetAuthorAsync(1)).Returns(Task.FromResult(existingAuthor));
            A.CallTo(() => fakeMapper.Map(authorForUpdateDto, existingAuthor));
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);
            A.CallTo(() => fakeMapper.Map<AuthorsForUpdateDto>(existingAuthor)).Returns(updatedAuthorDto);

            // Act
            var result = await _authorsController.UpdateAuthor(1, authorForUpdateDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<AuthorsForUpdateDto>().Subject;
            returnedDto.Name.Should().Be("Updated Name");
        }

        [Fact]
        public async Task UpdateAuthor_ShouldReturnNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = 1;
            var authorForUpdateDto = new AuthorsForUpdateDto
            {
                Name = "Updated Author",
                Biography = "author bio here"
            };

            A.CallTo(() => fakeRepository.GetAuthorAsync(authorId)).Returns(Task.FromResult<Authors>(null));

            // Act
            var result = await _authorsController.UpdateAuthor(authorId, authorForUpdateDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task PartiallyUpdateAuthor_ShouldReturnNoContent_WhenAuthorExists()
        {
            // Arrange

            var existingAuthor = new Authors { AuthorId = 1, Name = "Old Name" };
            var authorToPatch = new AuthorsForUpdateDto { Name = "Updated Name", Biography = "old bio" };
            var patchDocument = new JsonPatchDocument<AuthorsForUpdateDto>();
            patchDocument.Replace(a => a.Name, "Updated Name");

            A.CallTo(() => fakeRepository.GetAuthorAsync(1)).Returns(Task.FromResult(existingAuthor));
            A.CallTo(() => fakeMapper.Map<AuthorsForUpdateDto>(existingAuthor)).Returns(authorToPatch);
            A.CallTo(() => fakeMapper.Map(authorToPatch, existingAuthor));
            A.CallTo(() => fakeRepository.SaveChangesAsync()).Returns(true);

            // Act
            var result = await _authorsController.PartiallyUpdateAuthor(1, patchDocument);

            // Assert
            result.Should().BeOfType<NoContentResult>("because the author exists and should be successfully updated");
            A.CallTo(() => fakeRepository.SaveChangesAsync()).MustHaveHappened(); // Ensure SaveChangesAsync was called
        }

        [Fact]
        public async Task PartiallyUpdateAuthor_ShouldReturnNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
           
            A.CallTo(() => fakeRepository.GetAuthorAsync(1)).Returns(Task.FromResult<Authors>(null));

            var patchDocument = new JsonPatchDocument<AuthorsForUpdateDto>();

            // Act
            var result = await _authorsController.PartiallyUpdateAuthor(1, patchDocument);

            // Assert
            //result.Should().BeOfType<NotFoundResult>("because the author does not exist and should return NotFound");
            result.Should().BeOfType<NotFoundObjectResult>("because the author does not exist and should return NotFound");
        }

    }

}
