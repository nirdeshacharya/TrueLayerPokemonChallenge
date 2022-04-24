using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrueLayerChallenge.Models;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Constants;

namespace TrueLayerChallenge.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PokemonController : ControllerBase
	{
		readonly ILogger<PokemonController> _logger;
		readonly IPokemonDetailsService _pokemonDetail;
		readonly IPokemonDetailsTranslatedService _pokemonTranslatedDetail;

		public PokemonController(ILogger<PokemonController> logger, IPokemonDetailsService pokemonDetail, IPokemonDetailsTranslatedService pokemonTranslatedDetail)
		{
			_logger = logger;
			_pokemonDetail = pokemonDetail;
			_pokemonTranslatedDetail = pokemonTranslatedDetail;
		}

		[HttpGet("{name}")]
		public async Task<ActionResult> GetTranslated(string name, CancellationToken cancellationToken)
		{
			var summary = await _pokemonDetail.GetDetails(name, cancellationToken);

			if (summary == null)
				return NotFound();

			var translationType = ApplicationConstants.TranslationType;

			try
			{
				var translation = await _pokemonTranslatedDetail.GetTranslatedDetails(summary.Description, translationType, cancellationToken);
				if (translation != null)
					summary.Description = translation;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Failed to get {translationType} translation for \"{summary.Description}\".");
			}

			return Ok(summary);
		}
	}
}
