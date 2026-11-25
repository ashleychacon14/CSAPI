using Microsoft.AspNetCore.Mvc;
using apipractice.Models;
using apipractice.Services;

namespace apipractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return Ok(_todoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todo = _todoService.GetById(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpGet("status")]
        public ActionResult<List<TodoItem>> GetByStatus(
            [FromQuery] bool completed)
        {
            Console.WriteLine($"Query string: {Request.QueryString}");
            Console.WriteLine($"Controller received: {completed}");

            var todos = _todoService.GetByStatus(completed);

            return Ok(todos);
        }

        [HttpGet("search")]
        public ActionResult<List<TodoItem>> Search(
            [FromQuery] string title)
        {
            var todos = _todoService.GetByTitle(title);

            return Ok(todos);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todo)
        {
            var createdTodo = _todoService.Create(todo);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTodo.Id },
                createdTodo
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            bool updated = _todoService.Update(id, updatedTodo);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _todoService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}