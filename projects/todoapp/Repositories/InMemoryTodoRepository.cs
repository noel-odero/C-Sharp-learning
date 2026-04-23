using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly Dictionary<Guid, TodoItem> _store = new();

        public void Add(TodoItem item)
        {
            if(_store.ContainsKey(item.Id)) throw new InvalidOperationException($"A todo with ID {item.Id} already exists");
            _store[item.Id] = item;
        }

        public IEnumerable<TodoItem> GetAll() => _store.Values;

        public TodoItem? GetById(Guid id)
        {
            _store.TryGetValue(id, out var item);
            return item;
        }

        public void Update(TodoItem item)
        {
            if(!_store.ContainsKey(item.Id)) throw new KeyNotFoundException($"Todo with ID {item.Id} not found.");
            _store[item.Id] = item;
        }

        public void Delete(Guid id)
        {
            if(!_store.Remove(id)) throw new KeyNotFoundException($"Todo with ID {id} not found.");
        }
    }
}