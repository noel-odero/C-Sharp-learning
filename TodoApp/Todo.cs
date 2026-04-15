namespace TodoListApp
{

    public enum Priority {Low, Medium, High};
    public enum Status {Pending, InProgress, Done};
    public class TodoItem(string title, Priority priority,  string description="")
    {
        
        public string Title {get; set;} = title;
        public string Description{get; set;} = description;
        public Priority Priority{ get; private set;} = priority;
        public Status Status{get; private set;} = Status.Pending;
        public Guid Id{get; private set;} = Guid.NewGuid();
        public DateTime CreatedAt{get; private set;} = DateTime.Now;


        public string GetSummary()
        {
            return $"[{Status.ToString().ToUpper()} {Title} {Priority} - {CreatedAt}]";
        }
        
    }
}