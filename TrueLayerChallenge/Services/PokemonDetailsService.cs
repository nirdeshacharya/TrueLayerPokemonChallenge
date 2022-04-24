using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Models;
using System.Threading;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
using System.Linq;
using TrueLayerChallenge.Constants;

namespace TrueLayerChallenge.Services
{
	public class PokemonDetailsService : IPokemonDetailsService
	{
		public async Task<PokemonSummaryModel> GetDetails(string name, CancellationToken cancellationToken)
		{
			var context = new HttpClient
			{
				BaseAddress = new Uri(UriConstants.PokemonDetailUri)
			};

			using var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon-species/{name}");
			var response = await context.SendAsync(request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var speciesResponse = await response.Content.ReadFromJsonAsync<SpeciesResponse>(new(JsonSerializerDefaults.Web), cancellationToken);

				if (speciesResponse == null)
					return null;

				return new PokemonSummaryModel
				{
					Name = speciesResponse.name,
					Description = speciesResponse.flavor_text_entries.First(x => x.language.name == "en").flavor_text,
				};
			}

			if (response.StatusCode == HttpStatusCode.NotFound)
				return null;

			throw new HttpRequestException(@$"{request.Method} request to {request.RequestUri} failed with status code {response.StatusCode}.");
		}
	}
}
