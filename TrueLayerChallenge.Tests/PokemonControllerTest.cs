using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrueLayerChallenge.Controllers;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Services;
using Xunit;

namespace TrueLayerChallenge.Tests
{
	public class PokemonControllerTest
	{
        readonly IPokemonDetailsService _pokemonDetail;
        readonly IPokemonDetailsTranslatedService _pokemonTranslatedDetail;
        public PokemonControllerTest()
		{
            _pokemonDetail = new PokemonDetailsService();
            _pokemonTranslatedDetail = new PokemonDetailsTranslatedService();
        }
        [Fact]
        public async Task GetTranslated_ShouldReturn200Status()
        {
            // Arrange
            var cts = new CancellationToken();
            var pokemonName = "pikachu";
            var loggerFactory = new LoggerFactory();
            ILogger<PokemonController> logger = loggerFactory.CreateLogger<PokemonController>();
            var controller = new PokemonController(logger, _pokemonDetail, _pokemonTranslatedDetail);

            // Act
            var result = (OkObjectResult)await controller.GetTranslated(pokemonName, cts);


            // Assert
            result.StatusCode.Should().Be(200);
        }

    }
}
