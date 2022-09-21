using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Movies.Contracts.ContractModels;

namespace Movies.Server.Gql.Types
{
	public sealed class MovieType : ObjectGraphType<Movie>
	{
		public MovieType()
		{
			Name = "Movie_Data";
			Description = "Movie Data for Graph";

			Field(x => x.Id).Description("int ID");
			Field(x => x.Key, false).Description("Key");
			Field(x => x.Name).Description("Name.");
			Field(x => x.Description);
			Field(x => x.Genres);
			Field(x => x.Rate);
			Field(x => x.Length);
			Field(x => x.Img);
		}
	}
}
