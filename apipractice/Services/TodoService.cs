using apipractice.Models;

namespace apipractice.Services
{
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> todos = new()
        {
            new TodoItem
            {
                Id = 1,
                Title = "Learn C#",
                isComplete = false
            }
        };

        private int nextId = 2;

        public List<TodoItem> GetAll()
        {
            return todos;
        }

        public TodoItem? GetById(int id)
        {
            return todos.FirstOrDefault(t => t.Id == id);
        }

        public List<TodoItem> GetByTitle(string title)
        {
            return todos
                .Where(t => t.Title.Contains(
                    title,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<TodoItem> GetByStatus(bool completed)
        {
            return todos
                .Where(t => t.isComplete == completed)
                .ToList();
        }

        public List<TodoItem> GetByTitleAndStatus(string title, bool completed)
        {
            return todos
                .Where(t => t.Title.Contains(
                    title,
                    StringComparison.OrdinalIgnoreCase) &&
                    t.isComplete == completed)
                .ToList();
        }

        public TodoItem Create(TodoItem todo)
        {
            todo.Id = nextId++;

            todos.Add(todo);

            return todo;
        }

        public bool Update(int id, TodoItem updatedTodo)
        {
            var todo = GetById(id);

            if (todo == null)
                return false;

            todo.Title = updatedTodo.Title;
            todo.isComplete = updatedTodo.isComplete;

            return true;
        }

        public bool Delete(int id)
        {
            var todo = GetById(id);

            if (todo == null)
                return false;

            todos.Remove(todo);

            return true;
        }

    }
}