using BahrsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Entities;

namespace Teste
{
    public class SalaDeCinemaControllerTests
    {

        [Test]
        public void Test_GetSalasDeCinema_ReturnsOk()
        {
            var controller = new MovieTheatersController();

            var response = controller.GetSalasDeCinema();

            Assert.That(response, Is.TypeOf<OkObjectResult>());

            var okResult = response as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<MovieTheater>>());
        }

    }
}