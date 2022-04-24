using System;
using System.Threading;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Interface
{
	public interface IPokemonDetailsTranslatedService
	{
		public Task<string> GetTranslatedDetails(string description, string translationType, CancellationToken cancellationToken);
	}
}
