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
		public async Task<PokemonSummary> GetDetails(string name, CancellationToken cancellationToken)
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

				return new PokemonSummary
				{
					Name = speciesResponse.name,
					Description = speciesResponse.flavor_text_entries.First(x => x.language.name == "en").flavor_text,
				};
			}

			if (response.StatusCode == HttpStatusCode.NotFound)
				return null;

			throw new HttpRequestException(@$"{request.Method} request to {request.RequestUri} failed with status code {response.StatusCode}.");
		}


		public class SpeciesResponse
		{
			public string name { get; set; }
			public Flavor_Text_Entries[] flavor_text_entries { get; set; }
		}
		public class Flavor_Text_Entries
		{
			public string flavor_text { get; set; }
			public Language language { get; set; }
			public Version version { get; set; }
		}
		public class Language
		{
			public string name { get; set; }
			public string url { get; set; }
		}
	}
}
