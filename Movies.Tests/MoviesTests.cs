using Movies.Contracts;
using Movies.Contracts.ContractModels;
using Movies.GrainClients;
using NUnit.Framework;
using Orleans.TestingHost;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Tests
{
    public class MoviesTests	
    {
		public TestCluster Cluster { get; private set; }
		IMovieGrainClient movieGrainClient;

		[SetUp]
        public void Setup()
        {
			var builder = new TestClusterBuilder();
			builder.AddSiloBuilderConfigurator<SiloConfig>();
			Cluster = builder.Build();
			Cluster.Deploy();
			movieGrainClient = new MovieGrainClient(Cluster.GrainFactory);
		}

		[TearDown]
		public void StopAll() => Cluster.Dispose();

		[Test]
		[Description("Tests state of grain")]
		public async Task TestGrains()
		{
			var movies = new List<Movie>();
			for (int i = 3 - 1; i >= 0; i--)
			{
				var grain = Cluster.GrainFactory.GetGrain<IMovieGrain>(i);
				var movie = new Movie()
				{
					Id = i,
					Name = $"movie{i}",
					Description = $"movie description{i}",
					Genres = new List<string>(new string[] { "comedy", "drama", "thriller" }),
					Img = $"mymovieurl{i}",
					Key = $"key-key-{i}",
					Length = $"100{i}",
					Rate = $"{i}"
				};
				movies.Add(movie);
				await grain.Set(movie);
			}

			for (int i = 3 - 1; i >= 0; i--)
			{
				var grain = Cluster.GrainFactory.GetGrain<IMovieGrain>(i);
				var grainState = await grain.Get();
				Assert.IsTrue(movies[i].Equals(grainState));
			}
		}

		[Test]
		public async Task TestGet()
		{
			var movie = new Movie()
			{
				Id = 98,
				Name = "Lost Town",
				Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
				Img = "imgUrlLink",
				Key = "lost-town-01",
				Length = "95min",
				Genres = new List<string>(new string[] { "horror" }),
				Rate = "6.5"
			};
			await movieGrainClient.Add(movie);
			var lostTown = await movieGrainClient.Get(98);	
			Assert.IsTrue(movie.Equals(lostTown));
		}

		[Test]
		[Description("New Movie Added Successfully")]
		public async Task TestAddMovie()
		{
			var moviesCountBefore = (await movieGrainClient.GetAll()).Count;
			await movieGrainClient.Add(new Movie()
			{
				Id = 0,
				Name = "test",
				Description = "test",
				Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }),
				Img = "testimageurl",
				Key = "test-key",
				Length = "test_length",
				Rate = "9"
			});
			var moviesCountAfter = (await movieGrainClient.GetAll()).Count;
			Assert.IsTrue((moviesCountAfter - moviesCountBefore) == 1);
		}
	}
}