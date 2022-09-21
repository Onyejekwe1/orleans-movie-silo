using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Contracts.ContractModels;
using Orleans;

namespace Movies.Contracts
{
	public interface IMovieGrain : IGrainWithIntegerKey
	{
		Task<Movie> Get();
		Task Set(Movie movie);
	}
}
