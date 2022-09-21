using System;
using System.Collections.Generic;
using System.Text;
using Orleans.TestingHost;
using Orleans.Hosting;
using Movies.Contracts;

namespace Movies.Tests
{
	internal class SiloConfig : ISiloConfigurator
	{
		
		public void Configure(ISiloBuilder siloBuilder) => siloBuilder.AddMemoryGrainStorage("Default");
	}
}
