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

        
    }

}