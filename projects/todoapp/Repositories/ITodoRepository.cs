using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(Guid id);
        void Update(TodoItem item);
        void Delete(Guid id);
    }
}