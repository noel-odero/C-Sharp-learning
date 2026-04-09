namespace HospitalManagementSystem
{
    public class Person(string firstName, string lastName, DateTime dateOfBrith, string email, string phoneNumber)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set;} = lastName;
        public DateTime DateOfBirth { get; set; } = dateOfBrith;
        public string Email { get; set; } = email;
        public string PhoneNumber { get; set; } = phoneNumber;
    }
}