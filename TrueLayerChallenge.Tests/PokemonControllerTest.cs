using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrueLayerChallenge.Controllers;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Models;
using TrueLayerChallenge.Services;
using Xunit;

namespace TrueLayerChallenge.Tests
{
	public class PokemonControllerTest
	{
        private readonly IPokemonDetailsService _pokemonDetail;
        private readonly IPokemonDetailsTranslatedService _pokemonTranslatedDetail;
        private readonly CancellationToken  cts;

        private readonly ILogger<PokemonController> logger;


        //Mocking is not done because it is a simple service method
        public PokemonControllerTest()
		{
            _pokemonDetail = new PokemonDetailsService();
            _pokemonTranslatedDetail = new PokemonDetailsTranslatedService();
            cts = new CancellationToken();
            var loggerFactory = new LoggerFactory();
            logger = loggerFactory.CreateLogger<PokemonController>();
        }

        [Fact]
        public async Task GetTranslated_ShouldReturn200Status_When_ActualPokemonIsInput()
        {
            // Arrange
            var pokemonName = "pikachu";
   
            var controller = new PokemonController(logger, _pokemonDetail, _pokemonTranslatedDetail);

            // Act
            var result = (OkObjectResult)await controller.GetTranslated(pokemonName, cts);

            // Assert
            Assert.NotNull(result);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetTranslated_ShouldReturn404Status_When_RandomPokemonIsInput()
        {
            // Arrange
            var pokemonName = "random";
          
            var controller = new PokemonController(logger, _pokemonDetail, _pokemonTranslatedDetail);

            // Act
            var result = await controller.GetTranslated(pokemonName, cts);


            // Assert
            Assert.NotNull(result);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetTranslated_ShouldReturnPokemonName_When_InputPokemonName()
        {
            // Arrange
            var pokemonNameList = new List<string> {
                "charizard", "pikachu", "charmander"
            };

            var controller = new PokemonController(logger, _pokemonDetail, _pokemonTranslatedDetail);

            
            foreach(var pokemonName in pokemonNameList)
			{
                // Act
                var result = (OkObjectResult)await controller.GetTranslated(pokemonName, cts);
                Assert.NotNull(result);


                // Assert
                var actualResult = result.Value.As<PokemonSummaryModel>();
                Assert.NotNull(actualResult);
                result.Value.As<PokemonSummaryModel>().Name.Equals(pokemonName);
            }
        }
    }
}
