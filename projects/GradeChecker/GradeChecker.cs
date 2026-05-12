using System;
using Microsoft.Win32.SafeHandles;


// Build a Simple Console App: “Student Grade Checker

// Goal: Practice variables, conditions, methods, and classes.

// Requirements:

// - Ask the user to enter:
//     - Student name
//     - Score (0–100)
// - Create a method that:
//     - Returns a grade (A, B, C, D, F) based on the score
// - Display:
//     - “John scored 75 → Grade: B”
// A (Excellent/90–100%), B (Good/80–89%), C (Satisfactory/70–79%), D (Poor/60–69%), and F (Fail/<60%).

namespace GradeCheckerSystem
{
    
    public class GradeChecker
    {
    public string Name{get; set;}
    public int Grade{get; set;} 

    public GradeChecker(string name, int grade)
        {
            Name = name;
            Grade = grade;

        }

        public string GetGrade()
        {
            return Grade switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F",
            };
        }

        public void DisplayResult()
        {
            Console.WriteLine($"{Name} scored {Grade} → Grade: {GetGrade()}");
        }
    }

 

}