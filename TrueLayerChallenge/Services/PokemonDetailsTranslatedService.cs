using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrueLayerChallenge.Constants;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Models;

namespace TrueLayerChallenge.Services
{
	public class PokemonDetailsTranslatedService : IPokemonDetailsTranslatedService
	{
		public async Task<string> GetTranslatedDetails(string description, string translationType, CancellationToken cancellationToken)
		{
			var context = new HttpClient
			{
				BaseAddress = new Uri(UriConstants.PokemonDetailTranslationUri)
			};

			using var request = new HttpRequestMessage(HttpMethod.Post, $"{translationType.ToLowerInvariant()}.json")
			{
				Content = JsonContent.Create(new
				{
					text = description
				})
			};
			var response = await context.SendAsync(request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var translationResponse = await response.Content.ReadFromJsonAsync<TranslationResponse>(new(JsonSerializerDefaults.Web), cancellationToken);

				if (translationResponse?.success?.total >= 1 && translationResponse.contents?.translated != null)
					return translationResponse.contents.translated;

				return null;
			}

			throw new HttpRequestException(@$"{request.Method} request to {request.RequestUri} failed with status code {response.StatusCode}.");
		}
	}
	
}
