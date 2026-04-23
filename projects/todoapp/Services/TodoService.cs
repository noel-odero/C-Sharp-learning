using TodoApp.Models;
using TodoApp.Repositories;


namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ItodoRepository repository)
        {
            _repository = repository;
        }

        public void AddTodo(string title, string? description = null, Priority priority = PriorityQueue.Medium, DateTime? dueDate = null)
        {
            var todo = new TodoItem(title, description, priority, dueDate);
            _repository.Add(todo);
        }

        public IEnumerable<TodoItem> GetAll() => _repository.GetAll();

        public IEnumerable<TodoItrm> GetCompleted() => _repository.GetAll().Where(t => t.IsCompleted);
        public IEnumerable<TodoItrm> GetPending() => _repository.GetAll().Where(t => !t.IsCompleted);

        private TodoItem GetById(Guid id)
        {
            return _repository.GetById(id)
                ?? throw new KeyNotFoundException($"Todo with ID {id} not found.");
        }
        
        public void ToggleStatus(Guid id)
        {
            var todo = GetById(id);
            todo.Toggle();
            _repository.Update(todo);
        }
        public void UpdateTitle(Guid id, string newTitle)
        {
            var todo = GetOrThrow(id);
            todo.UpdateTitle(newTitle);
            _repository.Update(todo);
        }

        public void UpdatePriority(Guid id, Priority priority)
        {
            var todo = GetOrThrow(id);
            todo.UpdatePriority(priority);
            _repository.Update(todo);
        }

        public void Delete(Guid id)
        {
            GetOrThrow(id);
            _repository.Delete(id);
        }
    }
}