//Student Menu Interface Implementation
using System;
using System.Collections.Generic;
using System.Text;
using UserInformation.Services;
using UserInformation.UserModels;

namespace CLI.LMS.UserSections
{
    public class StudentMenuSection
    {
        public void EnterMainMenu()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Student Main Menu:");
            Console.WriteLine("1. View Course Menu");
            Console.WriteLine("--------------------------");

            var choice = Console.ReadLine();
            
            if ("1".Equals(choice))
            {
                CourseMainMenu();
            }

        }

        //Issue 17 start: Create teacher sub-menu
        public void CourseMainMenu()
        {
            var courses = CourseServiceProxy.Current.Courses;

            if (courses == null || !courses.Any())
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("No courses available. Please wait for instructor to add a course first.");
                Console.WriteLine("--------------------------");
                return;
            }

            Console.WriteLine("\nAvailable Courses:");
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Id} - {c.Name}: {c.Code}");
            }

            Console.Write("\nEnter course Id (The number before the course name/code): ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int selectedId))
            {
                Console.WriteLine("Invalid Id.");
                return;
            }

            var selectedCourse = courses.FirstOrDefault(c => c.Id == selectedId);

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            ShowCourseInfo(selectedCourse);
        }
        public void ShowCourseInfo(Course course)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine($"\nCourse: {course.Name} - {course.Code}");
                Console.WriteLine("1. Unenroll from this course");
                Console.WriteLine("2. View Modules");
                Console.WriteLine("3. View Assignments");
                Console.WriteLine("4. View Roster");
                Console.WriteLine("5. View Course Schedule");
                Console.WriteLine("6. Back");


                var choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {//Issue #19 start: Main Course menu 
                 //May need actual instances to see if this works better
                    case "1":
                        UnenrollSelf(course);
                        running = false;
                        break;

                    case "2":
                        ShowModules(course);
                        break;

                    case "3":
                        ShowAssignments(course);
                        break;

                    case "4":
                        ShowRoster(course);
                        break;

                    case "5":
                        ShowSchedule(course);
                        break;

                    case "6":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        public void ShowModules(Course course)
        {
            if (!course.Modules.Any())
            {
                Console.WriteLine("No modules available.");
                return;
            }

            Console.WriteLine("\nModules:");
            foreach (var module in course.Modules)
            {
                Console.WriteLine(module.Name);
            }
        }

        public void ShowAssignments(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available.");
                return;
            }

            Console.WriteLine("\nAssignments:");
            foreach (var assignments in course.Assignments)
            {
                Console.WriteLine(assignments.Name);
            }
        }

        public void ShowRoster(Course course)
        {
            if (!course.Roster.Any())
            {
                Console.WriteLine("No roster available.");
                return;
            }

            Console.WriteLine("\nStudents:");
            foreach (var student in course.Roster)
            {
                Console.WriteLine($"{student.Id} - {student.Name}");
            }
        }

        public void ShowSchedule(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments scheduled.");
                return;
            }

            Console.WriteLine("\nCourse Schedule:");
            foreach (var assignment in course.Assignments)
            {
                Console.WriteLine($"{assignment.Name} - Due: {assignment.DueDate}");
            }
        }
        public void UnenrollSelf(Course course)
        {
            Console.Write("Enter your student Id to unenroll: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int studentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var student = course.Roster
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                Console.WriteLine("You are not enrolled in this course.");
                return;
            }

            course.Roster.Remove(student);

            Console.WriteLine("You have been unenrolled from the course.");
        }
    }
}
