using GradeCheckerSystem;


Console.Write("Enter your name: ");
string name = Console.ReadLine();

Console.Write("Enter your grade");
int grade = int.Parse(Console.ReadLine());


GradeChecker student = new GradeChecker(name, grade);
student.DisplayResult();

