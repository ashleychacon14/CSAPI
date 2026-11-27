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

        // [HttpGet]
        // public ActionResult<List<TodoItem>> GetAll()
        // {
        //     return Ok(_todoService.GetAll());
        // }

        [HttpGet]
        public ActionResult<PagedResponse> GetFew(int page=1, int pageSize=10)
        {
            if ((page < 1) || (pageSize < 1) || (pageSize > 100))
            {
                return BadRequest("Invalid page or pageSize");
            }
            return Ok(_todoService.GetFew(page, pageSize));
        }

        [HttpGet("{id:int}")]
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

        [HttpGet("search/status")]
        public ActionResult<List<TodoItem>> GetByTitleAndStatus(
            [FromQuery] string title,
            [FromQuery] bool completed)
        {

            var todos = _todoService.GetByTitleAndStatus(title, completed);

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

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            bool updated = _todoService.Update(id, updatedTodo);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _todoService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}