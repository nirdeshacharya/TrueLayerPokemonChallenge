using System;
using System.Threading;
using System.Threading.Tasks;
using TrueLayerChallenge.Models;

namespace TrueLayerChallenge.Interface
{
	public interface IPokemonDetailsService
	{
		public Task<PokemonSummary> GetDetails(string name, CancellationToken cancellationToken);
	}
}
