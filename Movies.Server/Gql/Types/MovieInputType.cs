using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Movies.Server.Gql.Types
{
	public class MovieInputType : InputObjectGraphType
	{
		public MovieInputType()
		{
			Field<StringGraphType>("id");
			Field<StringGraphType>("name");
			Field<StringGraphType>("key");
			Field<StringGraphType>("description");
			Field<ListGraphType<StringGraphType>>("genres");
			Field<StringGraphType>("rate");
			Field<StringGraphType>("length");
			Field<StringGraphType>("img");
		}
	}
}
