namespace HospitalManagementSystem
{
    public class Nurse(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string department, decimal salary, string wardAssignment, string shiftType ) : Staff(firstName, lastName, dateOfBirth, email, phoneNumber, department, salary)
    {
        public string WardAssignment { get; set; } = wardAssignment;
        public string ShiftType { get; private set; } = shiftType;
        public void SetShiftType(string newShift)
        {
            if (newShift == "Day" || newShift == "Night")
            {
                ShiftType = newShift;
            }
            else
            {
                Console.WriteLine("ShiftType must be 'Day' or 'Night'");
            }
        }

        public void AdministerMedication(string medication, string patientName)
        {
            Console.WriteLine($"Nurse. {LastName} administered {medication} to {patientName}");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"WardAssignment: {WardAssignment}");
            Console.WriteLine($"ShiftType: {ShiftType}");
        }
        public override void GenerateReport()
        {
            Console.WriteLine($"Nursing Report - Nurse {LastName} | Ward: {WardAssignment} | Shift: {ShiftType}");
        } 
    }
}