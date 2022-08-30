using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Scenario.Tests.Integration
{
    /// <summary>
    /// A base implementation for NET Core integration tests.
    /// </summary>
    internal abstract class IntegrationTests : WebApplicationFactory<Program>
	{
		/// <summary>
		/// Gets the <see cref="HttpClient"/> to use to interact with the test server.
		/// </summary>
		protected HttpClient Client { get; private set; }

		/// <summary>
		/// Sets up the fixture before running any of its tests.
		/// </summary>
		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			CreateInternalClient();
		}

		protected override IHost CreateHost(IHostBuilder builder)
		{
			builder.UseEnvironment("IntegrationTesting");
			return base.CreateHost(builder);
		}

		/// <summary>
		/// Sets up before each test runs.
		/// </summary>
		[SetUp]
		public virtual void SetUp()
		{
			Client = CreateClient();
		}

		/// <summary>
		/// Tears down the fixture after all its tests have run.
		/// </summary>
		[OneTimeTearDown]
		public virtual void OneTimeTearDown()
		{
			Client?.Dispose();
		}

		/// <summary>
		/// Creates a new HTTP client to interact with the test server.
		/// </summary>
		protected virtual HttpClient CreateInternalClient()
		{
			var client = CreateClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}
	}
}
