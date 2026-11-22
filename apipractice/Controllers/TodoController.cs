using Microsoft.AspNetCore.Mvc;
using apipractice.Models;

namespace apipractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController: ControllerBase
    {
        private static List<TodoItem> todos = new()
        {
            new TodoItem
            {
                Id = 1,
                Title = "Learn C#",
                isComplete=false
            }
        };
        private static int nextId = 2;

        [HttpGet] //GET:
        public ActionResult<List<TodoItem>> GetAll()
        {
            return Ok(todos);
        }

        [HttpGet("{id}")] //GET: 1
        public ActionResult<TodoItem> GetById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost] // POST: api
        public ActionResult<TodoItem> Create(TodoItem todo)
        {
            todo.Id = nextId ++;
            todos.Add(todo);

            return CreatedAtAction(
                nameof(GetById),
                new { id = todo.Id},
                todo
            );
        }

        [HttpPut("{id}")] 
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound();
            
            todo.Title = updatedTodo.Title;
            todo.isComplete = updatedTodo.isComplete;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound();
            
            todos.Remove(todo);

            return NoContent();
        }

    }
}