using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Contracts.ContractModels;

namespace Movies.Contracts
{
	public interface IMovieGrainClient
	{
		Task<Movie> Get(int id);
		Task<Movie> Add(Movie movie);
		Task<List<Movie>> GetAll();

		Task<List<Movie>> GetByGenre(string genre);

		Task<List<Movie>> GetTopX(int num);		

		Task<List<Movie>> Search(string searchParam);	

		Task<Movie> Update(int id, Movie movie);
	}
}
