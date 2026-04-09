using HospitalManagementSystem;

Person noel = new Person("Anjeline", "Noel", new DateTime(2004, 12, 26), "noel@email.com", "250794019723");

Console.WriteLine($"Name: {noel.FirstName} {noel.LastName}");
Console.WriteLine($"Date of Birth: {noel.DateOfBirth.ToString("MMMM dd, yyyy")}");
Console.WriteLine($"Email: {noel.Email}");
Console.WriteLine($"Phone: {noel.PhoneNumber}");


Patient nick = new Patient("Nick", "lemy", new DateTime(2004, 12, 26), "noel@email.com", "250794019723", "D+", "Dust");

nick.AddDiagnosis("Flu");
nick.AddDiagnosis("Malaria");
nick.AddDiagnosis("Allergy");

foreach (string diagnosis in nick.Diagnoses )
{
    Console.WriteLine(diagnosis);
}

Console.WriteLine($"Name: {nick.FirstName} {nick.LastName}");
Console.WriteLine($"Date of Birth: {nick.DateOfBirth.ToString("MMMM dd, yyyy")}");
Console.WriteLine($"Email: {nick.Email}");
Console.WriteLine($"Phone: {nick.PhoneNumber}");
Console.WriteLine($"Blood type : {nick.BloodType}");
Console.WriteLine($"Allergies: {nick.Allergies}");
Console.WriteLine($"Medical Record Number: {nick.MedicalRecordNumber}");

noel.SetAge(21);
Console.WriteLine(noel.Age);

