namespace TodoApp.Repositories
{
    public interface ItodoRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(Guid id);
        void Update(TodoItem item);
        void Delete(Guid id);
    }
}