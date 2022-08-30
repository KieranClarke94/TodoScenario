using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Scenario.Tests.Integration.Controllers
{
	[TestFixture]
    internal class CRUD_Tests : IntegrationTests
    {
		private static Guid TodoId = new Guid("3af3793c-cc58-455d-ae02-a81b683910c3");
		private Todo CreatedTodo { get; set; }

		[Test, Order(0)]
		public async Task Step00_GetTodos_ZeroTodos()
		{
			var response = await Client.GetAsync("/api/todos");
			response.EnsureSuccessStatusCode();

			var todos = await response.GetContentAsync<List<Todo>>();

			Assert.That(todos, Is.Not.Null);
			Assert.That(todos.Count, Is.EqualTo(0));
		}

		[Test, Order(0)]
		public async Task Step00_GetTodoById_NotFound()
		{
			var response = await Client.GetAsync($"/api/todos/{TodoId}");
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test, Order(1)]
		public async Task Step01_CreateTodo_Created()
		{
			var todo = new Todo() { Id = TodoId, Description = "A todo description", State = TodoState.Pending };

			var response = await Client.PostAsync("/api/todos", todo.ToHttpContent());
			response.EnsureSuccessStatusCode();

            CreatedTodo = await response.GetContentAsync<Todo>();

            Assert.That(CreatedTodo.Id, Is.EqualTo(TodoId));
            Assert.That(CreatedTodo.Description, Is.EqualTo("A todo description"));
            Assert.That(CreatedTodo.State, Is.EqualTo(TodoState.Pending));
        }

		[Test, Order(2)]
		public async Task Step02_GetTodos_OneTodo()
		{
			var response = await Client.GetAsync("/api/todos");
			response.EnsureSuccessStatusCode();

			var todos = await response.GetContentAsync<List<Todo>>();

			Assert.That(todos, Is.Not.Null);
			Assert.That(todos.Count, Is.EqualTo(1));

			Assert.That(todos[0].Id, Is.EqualTo(TodoId));
			Assert.That(todos[0].Description, Is.EqualTo("A todo description"));
			Assert.That(todos[0].State, Is.EqualTo(TodoState.Pending));
		}

		[Test, Order(2)]
		public async Task Step02_GetTodoById()
		{
			var response = await Client.GetAsync($"/api/todos/{TodoId}");
			response.EnsureSuccessStatusCode();

			var todo = await response.GetContentAsync<Todo>();

			Assert.That(todo, Is.Not.Null);
			Assert.That(todo.Id, Is.EqualTo(TodoId));
			Assert.That(todo.Description, Is.EqualTo("A todo description"));
			Assert.That(todo.State, Is.EqualTo(TodoState.Pending));
		}

		[Test, Order(3)]
		public async Task Step03_UpdateTodo_Updated()
		{
			CreatedTodo.State = TodoState.Complete;
			CreatedTodo.Description = "Updated description";

			var response = await Client.PutAsync($"/api/todos/{TodoId}", CreatedTodo.ToHttpContent());
			response.EnsureSuccessStatusCode();

			CreatedTodo = await response.GetContentAsync<Todo>();

			Assert.That(CreatedTodo.Id, Is.EqualTo(TodoId));
			Assert.That(CreatedTodo.Description, Is.EqualTo("Updated description"));
			Assert.That(CreatedTodo.State, Is.EqualTo(TodoState.Complete));
		}

		[Test, Order(4)]
		public async Task Step04_GetTodoById_CorrectlyUpdated()
		{
			var response = await Client.GetAsync($"/api/todos/{TodoId}");
			response.EnsureSuccessStatusCode();

			var todo = await response.GetContentAsync<Todo>();

			Assert.That(todo, Is.Not.Null);
			Assert.That(todo.Id, Is.EqualTo(TodoId));
			Assert.That(todo.Description, Is.EqualTo("Updated description"));
			Assert.That(todo.State, Is.EqualTo(TodoState.Complete));
		}
	}
}
