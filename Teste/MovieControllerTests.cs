using BahrsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Entities;

namespace Teste
{
    public class MovieControllerTests
    {
        [Test]
        public void Test_GetMovies_ReturnsOk()
        {
            var controller = new MovieController();

            var response = controller.GetMovies();

            Assert.That(response, Is.TypeOf<OkObjectResult>());

            var okResult = response as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<Movie>>());
        }

        [Test]
        public void Test_GetMovieById_ExistingId_ReturnsOk()
        {
            var controller = new MovieController();
            int existingId = 1;

            var response = controller.GetMovieById(existingId);

            Assert.That(response, Is.TypeOf<OkObjectResult>());

            var okResult = response as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<Movie>());
            var movie = okResult.Value as Movie;

            Assert.That(movie?.Id, Is.EqualTo(existingId));
        }

        [Test]
        public void Test_GetMovieById_NonExistingId_ReturnsNotFound()
        {
            var controller = new MovieController();
            int nonExistingId = 100;

            var response = controller.GetMovieById(nonExistingId);

            Assert.That(response, Is.TypeOf<NotFoundResult>());
        }
    }
}
