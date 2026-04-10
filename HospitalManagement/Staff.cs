namespace HospitalManagementSystem
{
    public class Staff(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string department, decimal salary): Person(firstName, lastName, dateOfBirth, email, phoneNumber)
    {
        public Guid EmployeeId { get; private set; } = Guid.NewGuid();
        public string Department { get; set; } = department;
        public decimal Salary { get; private set; } = salary;

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"EmployeeId: {EmployeeId}");
            Console.WriteLine($"Department: {Department}");
            Console.WriteLine($"Salary: {Salary}");
        }

    }
}