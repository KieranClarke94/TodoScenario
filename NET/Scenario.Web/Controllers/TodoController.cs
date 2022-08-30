using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Scenario.Web.Controllers;

/// <summary>
/// Describes a management controller for <see cref="Todo"/> entities
/// </summary>
[Route("api/todos")]
[EnableCors]
[ApiController, Produces("application/json")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class TodoController : Controller
{
    private static readonly List<Todo> Todos = new List<Todo>();

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoController"/>
    /// </summary>
    public TodoController()
    {
    }

    /// <summary>
    /// Retrieves a <see cref="Todo"/> that has the specified id
    /// </summary>
    [HttpGet("{todoId:guid}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public IActionResult GetById([FromRoute] Guid todoId)
    {
        if (todoId == default) return BadRequest("The todoId is required");

        var todo = Todos.Where(x => x.Id == todoId).FirstOrDefault();
        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    /// <summary>
    /// Retrieves a list of <see cref="Todo"/> entities
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public IActionResult List()
    {
        return Ok(Todos);
    }

    /// <summary>
    /// Updates a <see cref="Todo"/> entity
    /// </summary>
    [HttpPut("{todoId:guid}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public IActionResult Update([FromRoute] Guid todoId, [FromBody] Todo todo)
    {
        if (todo == null) return BadRequest("The todo is required");
        if (todoId == default || todoId != todo.Id) return BadRequest("The todoId is required and needs to match the todo being updated");
        if (todo.Id == default) return BadRequest("The todo must have a valid id");

        var index = Todos.FindIndex(x => x.Id == todoId);
        if (index == -1)
        {
            return NotFound();
        }

        Todos[index] = todo;

        return Ok(todo);
    }

    /// <summary>
    /// Creates a <see cref="Todo"/> entity
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody]Todo todo)
    {
        if (todo == null) return BadRequest("The todo is required");
        if (Todos.Any(x => x.Id == todo.Id)) return Conflict("This todo already exists");

        Todos.Add(todo);

        return CreatedAtAction(nameof(GetById), new { todoId = todo.Id }, todo);
    }
}
