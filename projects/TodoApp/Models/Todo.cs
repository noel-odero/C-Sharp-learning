namespace TodoApp.Models;

public enum Priority
    {
        Low, Medium, High
    }

    public class TodoItem
    {
        public int Id {get; private set;}
        public string Title {get; private set;}
        public bool IsCompleted{get; private set;}
        public DateTime CreatedAt { get; private set;}
        public DateTime? DueDate {get; private set;}
        public Priority Priority {get; private set;}


        public TodoItem(int id, string title, Priority priority, DateTime createdAt, DateTime? dueDate = null)
        {
            Id = id;
            Title = title;
            Priority = priority;
            DueDate = dueDate;
            IsCompleted = false;
            CreatedAt = createdAt;
        }

        public void MarkComplete() => IsCompleted = true;

        public void MarkIncomplete() => IsCompleted = false;
        public bool IsOverdue()
        {
            return DueDate.HasValue && !IsCompleted && DueDate.Value < DateTime.Now;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "YES" : "NO";
            string overdue = IsOverdue()? "(OVERDUE)": "";

            return $"{Id}. {Title} [{status}] | Priority: {Priority} | Due: {DueDate?.ToShortDateString() ?? "None"}{overdue}";
        }

    }
    