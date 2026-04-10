namespace HospitalManagementSystem
{
    public class  Inpatient(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bloodType, string allergies, int bedNumber, string wardName) : Patient(firstName, lastName, dateOfBirth, email, phoneNumber, bloodType, allergies)
    {
        public int BedNumber { get; set; } = bedNumber;
        public string WardName { get; set; } = wardName;
        public DateTime AdmissionDate { get; private set; } = DateTime.Today;

        public override string GetPatientType()
        {
            return "Inpatient";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Patient Type: {GetPatientType()}");
            Console.WriteLine($"Bed Number: {BedNumber}");
            Console.WriteLine($"Ward Name: {WardName}");
            Console.WriteLine($"Admission Date: {AdmissionDate:MMMM dd, yyyy}");
            Console.WriteLine("Diagnoses:");

            if (Diagnoses.Count == 0)
            {
                Console.WriteLine("- None");
                return;
            }

            foreach (string diagnosis in Diagnoses)
            {
                Console.WriteLine($"- {diagnosis}");
            }
        }

        
    }

}