using apipractice.Models;

namespace apipractice.Services
{
    public interface ITodoService
    {
        List<TodoItem> GetAll();

        TodoItem? GetById(int id);

        List<TodoItem> GetByTitle(string title);

        List<TodoItem> GetByStatus(bool completed);

        List<TodoItem>GetByTitleAndStatus(string title, bool completed);
        
        TodoItem Create(TodoItem todo);

        bool Update(int id, TodoItem updatedTodo);

        bool Delete(int id);
    }
}