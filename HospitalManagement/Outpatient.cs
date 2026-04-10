namespace HospitalManagementSystem
{
    public class  Outpatient(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bloodType, string allergies, DateTime appointmentDate, string referringDoctor) : Patient(firstName, lastName, dateOfBirth, email, phoneNumber, bloodType, allergies)
    {
        public DateTime AppointmentDate { get; set; } = dateOfBirth.ToString("MMMM dd, yyyy");
        public string ReferringDoctor { get; set; } = referringDoctor;

        public override string GetPatientType()
        {
            return "Outpatient";
        }

        
    }

}