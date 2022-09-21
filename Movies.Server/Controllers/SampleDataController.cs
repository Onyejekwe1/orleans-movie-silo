using Microsoft.AspNetCore.Mvc;
using Movies.Contracts;
using Movies.Contracts.ContractModels;
using System.Threading.Tasks;

namespace Movies.Server.Controllers
{
	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
		private readonly IMovieGrainClient _client;

		public SampleDataController(
			IMovieGrainClient client
		)
		{
			_client = client;
		}

		// GET api/sampledata/1234
		[HttpGet("{id}")]
		public async Task<Movie> Get(int id)
		{
			var result = await _client.Get(id).ConfigureAwait(false);
			return result;
		}

		// POST api/sampledata/1234
		//[HttpPost("{id}")]
		//public async Task Set([FromRoute] int id, [FromForm] string name)
		//	=> await _client.Add(id, name).ConfigureAwait(false);
	}
}