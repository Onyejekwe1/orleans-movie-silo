using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Movies.Contracts;
using Movies.Contracts.ContractModels;
using Movies.Server.Gql.Types;

namespace Movies.Server.Gql.App
{
	public class MovieMutation : ObjectGraphType
	{
		public MovieMutation(IServiceProvider provider)
		{
			Name = "Movie_Mutations";
			var movieClient = provider.GetRequiredService<IMovieGrainClient>();

			var movieArgument = new QueryArgument<MovieInputType> { Name = "movie", Description = "Updated Movie" };
			var nameArgument = new QueryArgument<IntGraphType> { Name = "id", Description = "Movie Id to be updated" };
			Field<MovieType>("update_Movie",
				arguments: new QueryArguments(nameArgument, movieArgument),
				resolve: context => movieClient.Update(context.GetArgument<int>("id"), context.GetArgument<Movie>("movie")));

			Field<MovieType>("add_Movie",
				arguments: new QueryArguments(movieArgument),
				resolve: context => movieClient.Add(context.GetArgument<Movie>("movie")));
		}
	}
}
