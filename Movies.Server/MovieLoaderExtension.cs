using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Movies.Contracts;
using Movies.Contracts.ContractModels;
using Newtonsoft.Json;

namespace Movies.Server
{
	public static class MovieLoaderExtension
	{
		public static async Task LoadMovies(IMovieGrainClient grainClient)
		{
			using var file = File.OpenText(@"./SeedData/" + "movies.json");
			var s = file.ReadToEnd();

			var movies = JsonConvert.DeserializeObject<List<Movie>>(s);
			                              

			for (var index = movies.Count - 1; index >= 0; index--)
			{
				var movie = movies[index];
				await grainClient.Add(movie);
			}
		}
	}

		/*public class MovieList	
		{
			[JsonProperty("movies")]
			public Movie[] Movies { get; set; }
		}

		public class Movie
		{
			[JsonProperty("id")]
			public long Id { get; set; }

			[JsonProperty("key")]
			public string Key { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("description")]
			public string Description { get; set; }

			[JsonProperty("genres")]
			public string[] Genres { get; set; }

			[JsonProperty("rate")]
			public string Rate { get; set; }

			[JsonProperty("length")]
			public string Length { get; set; }

			[JsonProperty("img")]
			public string Img { get; set; }
		}*/
}
