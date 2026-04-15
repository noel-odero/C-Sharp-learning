namespace HospitalManagementSystem
{
    public class Doctor(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string department, decimal salary, string specialization, string licenseNumber ) : Staff(firstName, lastName, dateOfBirth, email, phoneNumber, department, salary)
    {
        public string Specialization { get; set; } = specialization;
        public string LicenseNumber { get; set; } = licenseNumber;

        public void Prescribe(string medication, string patientName)
        {
            Console.WriteLine($"Dr. {LastName} prescribed {medication} to {patientName}");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"LicenseNumber: {LicenseNumber}");
        }

        public override void GenerateReport()
        {
            Console.WriteLine($"Medical Report - Dr.{LastName} | Specialization: {Specialization} | Department: {Department}");
        } 
    }
}