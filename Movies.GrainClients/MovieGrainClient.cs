using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Contracts;
using Movies.Contracts.ContractModels;
using Orleans;

namespace Movies.GrainClients
{
	public class MovieGrainClient : IMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;
		private readonly List<int> movieIds = new List<int>();				

		public MovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public Task<Movie> Get(int id) => _grainFactory.GetGrain<IMovieGrain>(id).Get();

		public async Task<Movie> Add(Movie movie)
		{
			var id = movieIds.Contains(movie.Id) ? movieIds.Max() + 1 : movie.Id;

			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			await grain.Set(movie);
			movieIds.Add(id);
			return await grain.Get();
		}

		public async Task<List<Movie>> GetAll()
		{
			var movies = new List<Movie>();
			foreach (var grain in movieIds.Select(i => _grainFactory.GetGrain<IMovieGrain>(i)))
			{
				var movie = await grain.Get();
				movies.Add(movie);
			}

			return movies;
		}

		public async Task<List<Movie>> GetByGenre(string genre)
		{
			//TODO Handle case sensitivity and spaces
			var movies = await GetAll();
			return movies.Where(m => m.Genres
				.Select(g => g)
				.Contains(genre))
				.Select(m => m)
				.ToList();
		}

		public async Task<List<Movie>> GetTopX(int num = 0)
		{
			var movies = await GetAll();
			return num == 0 || num > movies.Count() 
				? movies.OrderByDescending(m => float.Parse(m.Rate)).Take(5).ToList() 
				: movies.OrderByDescending(m => float.Parse(m.Rate)).Take(num).ToList();
		}

		public async Task<List<Movie>> Search(string searchParam)
		{
			//TODO Handle case sensitivity
			var movies = await GetAll();
			return movies
				.Where(m => m.Name
					.Contains(searchParam) || m.Description.Contains(searchParam))
				.Select(m => m)
				.ToList();
		}

		public async Task<Movie> Update(int id, Movie movie)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			await grain.Set(movie);
			return await grain.Get();
		}
	}
}
