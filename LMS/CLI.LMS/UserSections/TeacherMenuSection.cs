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
            Console.WriteLine("1. Add/Select a Course Menu");
            Console.WriteLine("--------------------------\n");

            var choice = Console.ReadLine();

            //if (choice.Equals("1"))
            
            if ("1".Equals(choice))
            {
                SubMenu();
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

        public void SubMenu()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Course Manager (Sub-Menu):");
            Console.WriteLine("1. Add a course");
            Console.WriteLine("2. Select existing course Menu");
            Console.WriteLine("--------------------------\n");

            var choice = Console.ReadLine();

            if ("1".Equals(choice))
            {
                var newCourse = CreateCourseRecord();
                CourseServiceProxy.Current.Add(newCourse);
            }
            else if ("2".Equals(choice))
            {
                CourseSelectorMenu();
            }
            
        }

        //Issue 17 start: Create teacher sub-menu
        public void CourseSelectorMenu()
        {
            var courses = CourseServiceProxy.Current.Courses;

            if (courses == null || !courses.Any())
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("No courses available. Please add a course first.");
                Console.WriteLine("--------------------------");
                //SubMenu();
                return;
            }

            Console.WriteLine("\nAvailable Courses:");
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Id} - {c.Name} {c.Code}");
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
        //May need actual instances to see if this works better
        public void ShowCourseInfo(Course course)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine($"\nCourse: {course.Name} - {course.Code}");
                Console.WriteLine("1. Enroll a student");
                Console.WriteLine("2. Unenroll a student");
                Console.WriteLine("3. View Modules");
                Console.WriteLine("4. Add assignment");
                Console.WriteLine("5. View assignments");
                Console.WriteLine("6. View roster");
                Console.WriteLine("7. View course schedule");
                Console.WriteLine("8. Back");


                var choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {//Issue #19 start: Main Course menu
                    case "1":
                        EnrollStudent(course);
                        break;

                    case "2":
                        UnenrollStudent(course);
                        break;

                    case "3":
                        ShowModules(course);
                        break;

                    case "4":
                        AddAssignment(course);
                        break;

                    case "5":
                        ShowAssignments(course);
                        break;

                    case "6":
                        ShowRoster(course);
                        break;

                    case "7":
                        ShowSchedule(course);
                        break;

                    case "8":
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
                Console.WriteLine("\n--------------------------");
                Console.WriteLine("No roster available (Try enrolling a student). ");
                Console.WriteLine("--------------------------");
                return;
            }

            Console.WriteLine("\n--------------------------");
            Console.WriteLine("\nStudents of this course:");
            Console.WriteLine("--------------------------");

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

        public void EnrollStudent(Course course)
        {
            var students = StudentServiceProxy.Current.Students;

            Console.WriteLine("\n--------------------------");
            Console.WriteLine("1. Enroll new student");
            Console.WriteLine("2. Enroll existing student");
            Console.WriteLine("--------------------------");


            var enrollChoice = Console.ReadLine();

            //Ceate new student
            if ("1".Equals(enrollChoice))
            {
                var newStudent = CreateStudentRecord();
                StudentServiceProxy.Current.Add(newStudent);

                //Shallow copy
                course.Roster.Add(newStudent);
                Console.WriteLine("\n--------------------------");
                Console.WriteLine($"Student {newStudent.Name} enrolled!");
                Console.WriteLine("--------------------------");
                return;
            }

            //Show existing students
            else if ("2".Equals(enrollChoice))
            {
                if (students == null || !students.Any())
                {
                    Console.WriteLine("\n--------------------------");
                    Console.WriteLine("No existing students found. Please create a new student.");
                    Console.WriteLine("--------------------------");
                    return;
                }
                
                Console.WriteLine("\nExisting Students:");
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.Id} - {s.Name}");
                }
                Console.WriteLine("\n--------------------------");
                Console.WriteLine("\nEnter student Id to enroll or enter any key to exit:");
                Console.WriteLine("--------------------------");
                var input = Console.ReadLine()?.Trim() ?? "";

                //Select existing student
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

                //Prevent duplicate enrollment
                if (course.Roster.Any(s => s.Id == selectedStudent.Id))
                {
                    Console.WriteLine("\n--------------------------");
                    Console.WriteLine("Student already enrolled in this course.");
                    Console.WriteLine("--------------------------");
                    return;
                }

                course.Roster.Add(selectedStudent);
                Console.WriteLine("\n--------------------------");
                Console.WriteLine($"Student {selectedStudent.Name} enrolled!");
                Console.WriteLine("--------------------------");
            }
        }
        //Issue #4 start: Unenrolling students
        public void UnenrollStudent(Course course)
        {
            if (!course.Roster.Any())
            {
                Console.WriteLine("No students enrolled in this course.");
                return;
            }

            Console.WriteLine("\nEnrolled Students:");
            foreach (var s in course.Roster)
            {
                Console.WriteLine($"{s.Id} - {s.Name}");
            }

            Console.Write("\nEnter student Id to unenroll (Based on course roster): ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int studentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var studentToRemove = course.Roster
                .FirstOrDefault(s => s.Id == studentId);

            if (studentToRemove == null)
            {
                Console.WriteLine("Student not found in this course.");
                return;
            }

            course.Roster.Remove(studentToRemove);

            Console.WriteLine($"Student {studentToRemove.Name} unenrolled.");
        }

        public void AddAssignment(Course course)
        {
            var newAssignment = new Assignment();

            Console.Write("Assignment Name: ");
            newAssignment.Name = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Description: ");
            newAssignment.Description = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Due Date (MM/DD/YYYY): ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!DateTime.TryParse(input, out DateTime dueDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            newAssignment.DueDate = dueDate;

            //Assign Id to assignment
            if (!course.Assignments.Any())
            {
                newAssignment.Id = "1";
            }
            else
            {
                newAssignment.Id = course.Assignments.Max(a => a.Id) + 1;
            }

            course.Assignments.Add(newAssignment);

            Console.WriteLine($"Assignment '{newAssignment.Name}' added!");
        }
    }
}
