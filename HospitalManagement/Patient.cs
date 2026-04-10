namespace HospitalManagementSystem
{
    public class Patient(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bloodType, string allergies) : Person(firstName, lastName, dateOfBirth, email, phoneNumber)
    {
        public string BloodType { get; set; } = bloodType;
        public string Allergies { get; set; } = allergies;
        public Guid MedicalRecordNumber { get; private set; } = Guid.NewGuid();
        
        public List<string> Diagnoses { get; private set; } = new();

        public void AddDiagnosis(string diagnosis)
        {
            Diagnoses.Add(diagnosis);
        }

        public virtual string GetPatientType()
        {
            return "Patient";
        }
    }
}