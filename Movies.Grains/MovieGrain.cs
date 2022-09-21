using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Contracts;
using Movies.Contracts.ContractModels;
using Orleans;
using Orleans.Providers;

namespace Movies.Grains
{
	[StorageProvider(ProviderName = "Default")]
	public class MovieGrain : Grain<Movie>, IMovieGrain
	{
		public Task<Movie> Get() => Task.FromResult(State);

		public Task Set(Movie movie)
		{
			State = movie;
			return Task.CompletedTask;
		}
	}
}
