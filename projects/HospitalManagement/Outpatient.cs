namespace HospitalManagementSystem
{
    public class  Outpatient(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bloodType, string allergies, DateTime appointmentDate, string referringDoctor) : Patient(firstName, lastName, dateOfBirth, email, phoneNumber, bloodType, allergies)
    {
        public DateTime AppointmentDate { get; set; } = appointmentDate;
        public string ReferringDoctor { get; set; } = referringDoctor;

        public override string GetPatientType()
        {
            return "Outpatient";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Patient Type: {GetPatientType()}");
            Console.WriteLine($"Appointment Date: {AppointmentDate:MMMM dd, yyyy}");
            Console.WriteLine($"Referring Doctor: {ReferringDoctor}");
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