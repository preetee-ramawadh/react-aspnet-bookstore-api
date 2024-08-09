﻿using Xunit;
using FluentAssertions;
using FakeItEasy;
using Bookstore.API.Controllers;
using Bookstore.API.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Bookstore.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Bookstore.API.Models;

namespace Bookstore.Test.Controller
{
    public class GenreControllerTest
    {
        private readonly GenresController genresController;
        private readonly IGenresInfoRepository genresInfoRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GenresController> logger;
        public GenreControllerTest()
        {
            //set up Dependencies
            this.genresInfoRepository = A.Fake<IGenresInfoRepository>();
            this.mapper = A.Fake<IMapper>();
            this.logger = A.Fake<ILogger<GenresController>>();

            //SUT --> System under Test
            this.genresController = new GenresController(this.logger, this.genresInfoRepository, this.mapper);
        }

        [Fact]
        public async Task GetGenres_ShouldReturnOk_WhenGenresAreAvailable()
        {
            // Arrange
            var genres = new List<Genres>
        {
            new Genres { GenreId = 1, GenreName = "Science Fiction" },
            new Genres { GenreId = 2, GenreName = "Fantasy" }
        };
            var genresDto = new List<GenresDto>
        {
            new GenresDto { GenreId = 1, GenreName = "Science Fiction" },
            new GenresDto { GenreId = 2, GenreName = "Fantasy" }
        };

            A.CallTo(() => genresInfoRepository.GetGenresAsync()).Returns(Task.FromResult<IEnumerable<Genres>>(genres));
            A.CallTo(() => mapper.Map<IEnumerable<GenresDto>>(genres)).Returns(genresDto);

            // Act
            var result = await genresController.GetGenres();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(genresDto);
        }

        [Fact]
        public async Task GetGenres_ShouldReturnOk_WhenNoGenresAreAvailable()
        {
            // Arrange
            var genres = new List<Genres>();
            var genresDto = new List<GenresDto>();

            A.CallTo(() => genresInfoRepository.GetGenresAsync()).Returns(Task.FromResult<IEnumerable<Genres>>(genres));
            A.CallTo(() => mapper.Map<IEnumerable<GenresDto>>(genres)).Returns(genresDto);

            // Act
            var result = await genresController.GetGenres();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(genresDto);
        }

        [Fact]
        public async Task GetGenre_ShouldReturnNotFound_WhenGenreIsNotFound()
        {
            // Arrange
            int genreId = 1;
            _ = A.CallTo(() => genresInfoRepository.GetGenreAsync(genreId)).Returns(Task.FromResult<Genres>(null));

            // Act
            var result = await this.genresController.GetGenre(genreId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
            A.CallTo(() => logger.LogInformation($"Genre with id {genreId} wasn't found when accessing genre.")).MustHaveHappened();
        }

        [Fact]
        public async Task GetGenre_ShouldReturnOk_WhenGenreIsFound()
        {
            // Arrange
            int genreId = 1;
            var genre = new Genres { GenreId = genreId, GenreName = "Sci-Fi" };
            var genresDto = new GenresDto { GenreId = genreId, GenreName = "Sci-Fi" };

            A.CallTo(() => genresInfoRepository.GetGenreAsync(genreId)).Returns(Task.FromResult(genre));
            A.CallTo(() => mapper.Map<GenresDto>(genre)).Returns(genresDto);

            // Act
            var result = await genresController.GetGenre(genreId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result.As<OkObjectResult>();
            okResult.Value.Should().BeEquivalentTo(genresDto);
        }
    }
}
