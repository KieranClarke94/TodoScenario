using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Scenario.Tests.Integration.Controllers
{
	[TestFixture]
    internal class Validation_Tests : IntegrationTests
    {
		private static Guid TodoId = new Guid("3af3793c-cc58-455d-ae02-a81b683910c2");

		[Test, Order(0)]
		public async Task Step00_GetTodoById_BadRequest()
		{
			var response = await Client.GetAsync($"/api/todos/{Guid.Empty}");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test, Order(1)]
		[TestCase("")]
		[TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean quis urna condimentum, vulputate lectus et, bibendum ex. Sed elementum leo purus, vitae commodo eros posuere non. Etiam non ultrices ante. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida.")]
		public async Task Step01_CreateTodo_BadRequest(string desc)
		{
			var todo = new Todo() { Id = TodoId, Description = desc ?? "A todo description", State = TodoState.Pending };

			var response = await Client.PostAsync("/api/todos", desc == "null" ? null : todo.ToHttpContent());
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test, Order(2)]
		public async Task Step01_CreateTodo_Conflict()
		{
			var todo = new Todo() { Id = TodoId, Description = "A todo description", State = TodoState.Pending };

			var response = await Client.PostAsync("/api/todos", todo.ToHttpContent());
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

			response = await Client.PostAsync("/api/todos", todo.ToHttpContent());
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
		}

		[Test, Order(3)]
		[TestCase("")]
		[TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean quis urna condimentum, vulputate lectus et, bibendum ex. Sed elementum leo purus, vitae commodo eros posuere non. Etiam non ultrices ante. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida. Praesent a bibendum tortor, eu maximus gravida.")]
		public async Task Step03_UpdateTodo_Description_BadRequest(string desc)
		{
			var todo = new Todo() { Id = TodoId, Description = desc, State = TodoState.Pending };

			var response = await Client.PutAsync($"/api/todos/{TodoId}", todo.ToHttpContent());
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}
	}
}
