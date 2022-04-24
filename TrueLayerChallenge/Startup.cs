using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrueLayerChallenge.Interface;
using TrueLayerChallenge.Services;

namespace TrueLayerChallenge
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddTransient<IPokemonDetailsService, PokemonDetailsService>();
			services.AddTransient<IPokemonDetailsTranslatedService, PokemonDetailsTranslatedService>();

			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
