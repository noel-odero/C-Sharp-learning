using HospitalManagementSystem;

Doctor doctor = new Doctor(
    "Anjeline",
    "Noel",
    new DateTime(1990, 12, 26),
    "doctor@email.com",
    "250794019723",
    "General Medicine",
    2500m,
    "Internal Medicine",
    "LIC-DR-001");

Nurse nurse = new Nurse(
    "Awino",
    "Lena",
    new DateTime(1995, 8, 12),
    "nurse@email.com",
    "250700000001",
    "Emergency",
    1600m,
    "Ward A",
    "Day");

Inpatient inpatient = new Inpatient(
    "Odero",
    "Mark",
    new DateTime(2001, 4, 5),
    "inpatient@email.com",
    "250700000002",
    "O+",
    "Penicillin",
    12,
    "Ward A");

Outpatient outpatient = new Outpatient(
    "Angie",
    "Faith",
    new DateTime(2000, 7, 18),
    "outpatient@email.com",
    "250700000003",
    "A-",
    "None",
    DateTime.Today.AddDays(2),
    "Dr. Noel");

doctor.DisplayInfo();
doctor.Prescribe("Amoxicillin", inpatient.FirstName);
Console.WriteLine();

nurse.DisplayInfo();
nurse.SetShiftType("Night");
nurse.AdministerMedication("Paracetamol", inpatient.FirstName);
Console.WriteLine();

inpatient.AddDiagnosis("Malaria");
inpatient.AddDiagnosis("Anemia");
inpatient.DisplayInfo();
Console.WriteLine();

outpatient.AddDiagnosis("Seasonal Allergy");
outpatient.DisplayInfo();

List<Staff> staff  = new List<Staff>
{
    new Doctor(
        "Brian",
        "Otieno",
        new DateTime(1988, 3, 14),
        "brian.otieno@hospital.com",
        "250700100001",
        "Surgery",
        3200m,
        "General Surgery",
        "LIC-DR-002"),
    new Doctor(
        "Sarah",
        "Achieng",
        new DateTime(1991, 11, 2),
        "sarah.achieng@hospital.com",
        "250700100002",
        "Pediatrics",
        3000m,
        "Pediatric Medicine",
        "LIC-DR-003"),
    new Nurse(
        "Kevin",
        "Mumo",
        new DateTime(1994, 6, 23),
        "kevin.mumo@hospital.com",
        "250700200001",
        "Maternity",
        1700m,
        "Ward B",
        "Day"),
    new Nurse(
        "Diana",
        "Njeri",
        new DateTime(1996, 1, 9),
        "diana.njeri@hospital.com",
        "250700200002",
        "ICU",
        1850m,
        "ICU-1",
        "Night")
};

foreach (Staff member in staff)
{
    member.GenerateReport();
}

