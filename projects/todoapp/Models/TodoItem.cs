namespace TodoApp.Models
{
    public enum Priority{Low, Medium, High}

    public class TodoItem
    {
        public Guid Id {get; init;}
        public string Title{get; private set;}
        public string? Description { get; private set;}
        public bool IsCompleted{get; private set;}
        public Priority Priority{get; private set;}
        public DateTime CreatedAt {get; init;}
        public DateTime? DueDate{get; private set;}


        public TodoItem(string title, string? description = null, Priority priority = Priority.Medium, DateTime? dueDate = null)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }

        public void Complete() => IsCompleted = true;
        public void Toggle() => IsCompleted = !IsCompleted;

        public void UpdateTitle(string newTitle)
        {
            if(string.IsNullOrWhiteSpace(newTitle)) throw new ArgumentException("Title cannot be empty.", nameof(newTitle));
            Title = newTitle;
        }

        public void UpdatePriority(Priority priority) => Priority = priority;

        public override string ToString()
        {
            return $"[{(IsCompleted ? "✓" : " ")}] {Title} | {Priority} | Due: {DueDate?.ToString("yyyy-MM-dd") ?? "None"}";
        }
    }
}