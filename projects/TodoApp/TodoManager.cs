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

        
    }
}