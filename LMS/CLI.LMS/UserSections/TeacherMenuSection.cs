using UserInformation.UserModels;
using CLI.LMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserInformation.Services;

namespace CLI.LMS.UserSections
{
    public class TeacherMenuSection
    {
        public void EnterMainMenu()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Teacher Main Menu:");
            Console.WriteLine("1. Enroll a student");
            Console.WriteLine("2. Add a course");
            Console.WriteLine("--------------------------\n");

            var choice = Console.ReadLine();

            //if (choice.Equals("1"))
            if ("1".Equals(choice))
            {
                var newStudent = CreateStudentRecord();
                StudentServiceProxy.Current.Add(newStudent);
            }
            else if ("2".Equals(choice))
            {
                var newCourse = CreateCourseRecord();
                CourseServiceProxy.Current.Add(newCourse);
            }
        }

        public Student CreateStudentRecord()
        {
            var newStudent = new Student();
            Console.Write("Name: ");
            newStudent.Name = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Code: ");
            newStudent.Code = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine("Classification: ");
            Console.WriteLine("F. Freshmen");
            Console.WriteLine("S. Sophomore");
            Console.WriteLine("J. Junior");
            Console.WriteLine("R. Senior");
            Console.WriteLine("U. Unknown");
            var classChoice = Console.ReadLine()?.Trim() ?? "";
            if (classChoice.Equals("F", StringComparison.InvariantCultureIgnoreCase))
            {
                newStudent.Classification = Classification.Freshman;

            }
            else if (classChoice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                newStudent.Classification = Classification.Sophomore;
            }
            else if (classChoice.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                newStudent.Classification = Classification.Junior;
            }
            else if (classChoice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
            {
                newStudent.Classification = Classification.Senior;
            }
            else
            {
                newStudent.Classification = Classification.Unknown;
            }

            return newStudent;
        }
        public Course CreateCourseRecord()
        {
            var newCourse = new Course();
            Console.Write("Name: ");
            newCourse.Name = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Code: ");
            newCourse.Code = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Description: ");
            newCourse.Description = Console.ReadLine()?.Trim() ?? "";

            return newCourse;
        }
    }
}
