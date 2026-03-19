//Student Menu Interface Implementation
using CLI.LMS.Services;
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
            Console.WriteLine("\n--------------------------");
            Console.WriteLine("Student Main Menu:");
            Console.WriteLine("1. Select a student");
            Console.WriteLine("--------------------------");

            var choice = Console.ReadLine();
            
            if ("1".Equals(choice))
            {
                SelectStudent();
            }

        }

        //Issue 17 start: Create teacher sub-menu
        public void CourseSelectorMenu(Student student)
        {
            var allCourses = CourseServiceProxy.Current.Courses;
            var enrolledCourses = allCourses.Where(c => c.Roster.Any(s => s.Id == student.Id)).ToList();

            if (enrolledCourses == null || !enrolledCourses.Any())
            {
                Console.WriteLine("/n--------------------------");
                Console.WriteLine("No courses available. Please wait for instructor to add a course first.");
                Console.WriteLine("--------------------------");
                return;
            }

            Console.WriteLine($"\nCourses for {student.Name}:");

            foreach (var c in enrolledCourses)
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

            var selectedCourse = enrolledCourses.FirstOrDefault(c => c.Id == selectedId);

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
            Console.Write("Enter your student Id to unenroll OR type a letter to go back: ");
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
        //Issue #66 start (Student login).
        //Also closes #2 (Allow student to interact with enrolled courses).
        private void SelectStudent()
        {
            var students = StudentServiceProxy.Current.Students;

            if (students == null || !students.Any())
            {
                //Future: Make it so students can enroll themselves?
                Console.WriteLine("\n--------------------------");
                Console.WriteLine("No students found. Please wait for instructor to enroll a student first.");
                Console.WriteLine("--------------------------");
                return;
            }

            Console.WriteLine("\nStudents:");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} - {s.Name}");
            }

            Console.Write("\nEnter student Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int studentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var selectedStudent = students.FirstOrDefault(s => s.Id == studentId);

            if (selectedStudent == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            CourseSelectorMenu(selectedStudent);
        }
    }
}
