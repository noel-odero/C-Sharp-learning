namespace HospitalManagementSystem
{
    public abstract class Person(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set;} = lastName;
        public DateTime DateOfBirth { get; set; } = dateOfBirth;
        public string Email { get; set; } = email;
        public string PhoneNumber { get; set; } = phoneNumber;
        public int Age { get; private set; }

        public void SetAge(int age)
        {
            if(age < 0)
            {
                Console.WriteLine("Age cannot be negative");
                return;
            }
            Age = age;

        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Date of Birth: {DateOfBirth.ToString("MMMM dd, yyyy")}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {PhoneNumber}");
        }
        
    }
}