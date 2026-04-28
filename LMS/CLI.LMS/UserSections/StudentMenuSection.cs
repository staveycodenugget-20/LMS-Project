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

            ShowCourseInfo(selectedCourse, student);
        }
        public void ShowCourseInfo(Course course, Student student)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine($"\nCourse: {course.Name} - {course.Code}");
                Console.WriteLine("1. Unenroll from this course");
                Console.WriteLine("2. View Modules");
                Console.WriteLine("3. View Assignments");
                Console.WriteLine("4. Submit Assignment");
                Console.WriteLine("5. View Roster");
                Console.WriteLine("6. View Course Schedule");
                Console.WriteLine("7. Back");


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
                        ShowAssignments(course, student);
                        break;

                    case "4":
                        SubmitAssignment(course, student);
                        break;

                    case "5":
                        ShowRoster(course);
                        break;

                    case "6":
                        ShowSchedule(course);
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        //Updte for issue #19: Students can't see module content, but can see module names.
        public void ShowModules(Course course)
        {
            if (!course.Modules.Any())
            {
                Console.WriteLine("No modules available.");
                return;
            }

            foreach (var module in course.Modules)
            {
                Console.WriteLine($"\nModule: {module.Name}");

                if (!module.Contents.Any())
                {
                    Console.WriteLine("  No content.");
                    continue;
                }

                foreach (var item in module.Contents)
                {
                    if (item is PageItem page)
                    {
                        Console.WriteLine($"  [Page] {page.Title}");
                        Console.WriteLine($"    {page.Content}");
                    }
                    else if (item is FileItem file)
                    {
                        Console.WriteLine($"  [File] {file.Title} (Path: {file.FilePath})");
                        Console.WriteLine($"Opening file: {file.FilePath}");
                    }
                    else if (item is AssignmentItem assignmentItem)
                    {
                        Console.WriteLine($"  [Assignment] {assignmentItem.Assignment?.Name}");
                    }
                }
            }
        }

        public void ShowAssignments(Course course, Student student)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available.");
                return;
            }

            Console.WriteLine("\nAssignments:");

            foreach (var assignment in course.Assignments)
            {
                Console.WriteLine($"\n{assignment.Id} - {assignment.Name}");

                var submission = assignment.Submissions
                    .FirstOrDefault(s => s.StudentId == student.Id);

                if (submission != null)
                {
                    Console.WriteLine($"  Submitted: {submission.SubmissionDate}");

                    if (submission.Grade.HasValue)
                    {
                        Console.WriteLine($"  Grade: {submission.Grade}");
                    }
                    else
                    {
                        Console.WriteLine("  Grade: Not graded yet");
                    }
                }
                else
                {
                    Console.WriteLine("  No submission yet");
                }
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

        public void SubmitAssignment(Course course, Student student)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available.");
                return;
            }

            Console.WriteLine("\nAssignments:");
            foreach (var a in course.Assignments)
            {
                Console.WriteLine($"{a.Id} - {a.Name}");
            }

            Console.Write("\nEnter Assignment Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int assignmentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var assignment = course.Assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            Console.Write("Enter submission content: ");
            var content = Console.ReadLine()?.Trim() ?? "";

            var submission = new Submission
            {
                StudentId = student.Id,
                AssignmentId = assignment.Id,
                Content = content,
                SubmissionDate = DateTime.Now,
                Id = assignment.Submissions.Any() ? assignment.Submissions.Max(s => s.Id) + 1 : 1
            };

            assignment.Submissions.Add(submission);

            Console.WriteLine("Submission successful!");
        }
    }
}
