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


        public abstract string GetSummary();


        
    }

    public class DeadlinedTask(string title, Priority priority,   DateTime dueDate, string description=""): TodoItem(title, priority, description)
    {
        public DateTime DueDate {get; set;} = dueDate;

        public bool IsOverDue()
        {
            return DateTime.Now > DueDate && Status != Status.Done;
        }
        public override string GetSummary()
        {
            return $"[{Status.ToString().ToUpper()}] {Title} {Priority} - Created at: {CreatedAt}, Duedate: {DueDate}, overdue: {IsOverDue}";
        }

    }

    public class RecurringTast(string title, Priority priority, int repeatEveryDays, string description="") : TodoItem(title, priority, description)
    {
        public int RepeatEveryDays {get;  set; } = repeatEveryDays;
        public DateTime NextOccurrence => CreatedAt.AddDays(RepeatEveryDays);

        public override string GetSummary()
        {
            return $"[{Status.ToString().ToUpper()}] {Title} {Priority} - Created at: {CreatedAt}, NextOccurrenceOn: {NextOccurrence}]";
        }

    }





}