using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL.Utilities;
using Movies.Contracts;
using Movies.Server.Gql.Types;

namespace Movies.Server.Gql.App
{
	public class MovieQuery : ObjectGraphType
	{
		public MovieQuery(IServiceProvider provider)
		{
			var movieClient = provider.GetRequiredService<IMovieGrainClient>();
			Name = "MovieQueries";

			Field<ListGraphType<MovieType>>("All_Movies", resolve: context => movieClient.GetAll());

			Field<MovieType>("Get_Single_Movie", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "id"
			}), resolve: context => movieClient.Get(int.Parse(context.Arguments["id"].ToString().Trim())));

			Field<ListGraphType<MovieType>>("getMoviesByGenre", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "genre"
			}), resolve: context => movieClient.GetByGenre(context.Arguments["genre"].ToString()));

			Field<ListGraphType<MovieType>>("topX", resolve: context => movieClient.GetTopX(int.Parse(context.Arguments["id"].ToString().Trim())));

			Field<ListGraphType<MovieType>>("search", "Search Either By Title And/Or Description", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "searchParam"
			}), resolve: context => movieClient.Search(context.Arguments["searchParam"].ToString()));
		}
	}
}
