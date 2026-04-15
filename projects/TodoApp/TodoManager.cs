namespace TodoListApp
{
    public class TodoManager
    {
        private readonly List<TodoItem> _items = new();


        public void Add(TodoItem item)
        {
            if(item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _items.Add(item);
        }

        public bool Remove(Guid id)
        {
            TodoItem? item = _items.FirstOrDefault(x=> x.Id == id);
            if(item is null)
            {
                return false;
            }
            _items.Remove(item);
            return true;
        }

        public List<TodoItem> GetAll()
        {
            return _items.ToList();
        }

        public TodoItem? GetById(Guid id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public List<TodoItem> GetByStatus(Status status)
        {
            return _items.Where(x => x.Status == status).ToList();
        }

        public List<TodoItem> GetByPriority(Priority priority)
        {
            return _items.Where(x => x.Priority == priority ).ToList();
        }

        public void Update(Guid id, string newTitle, string newDescription, PriorityQueue newPriority)
        {
            TodoItem? item = _items.FirstOrDefault(x => x.Id == id);
            if(item is null)
            {
                throw new InvalidOperationException($"Todo with id {id} not found");
            }

            item.Title = newTitle;
            item.Description = newDescription;
            item.Priority = newPriority;
        }

        
    }
}