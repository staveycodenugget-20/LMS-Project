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
            Console.WriteLine("1. Course Manager Menu");
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
            Console.WriteLine("2. Remove a course");
            Console.WriteLine("3. Select existing course Menu");
            Console.WriteLine("--------------------------\n");

            var choice = Console.ReadLine();

            if ("1".Equals(choice))
            {
                var newCourse = CreateCourseRecord();
                CourseServiceProxy.Current.Add(newCourse);
            }
            else if ("2".Equals(choice))
            {
                DeleteCourse();
            }
            else if ("3".Equals(choice))
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
                Console.WriteLine("3. Edit course description");
                Console.WriteLine("4. View modules");
                Console.WriteLine("5. Add modules");
                Console.WriteLine("6. Add module content");
                Console.WriteLine("7. Edit modules");
                Console.WriteLine("8. Remove module");
                Console.WriteLine("9. Add assignment");
                Console.WriteLine("10. Edit assignment");
                Console.WriteLine("11. View assignments");
                Console.WriteLine("12. Manage assignment groups");
                Console.WriteLine("13. Delete assignments");
                Console.WriteLine("14. Grade submissions");
                Console.WriteLine("15. View roster");
                Console.WriteLine("16. View course schedule");
                Console.WriteLine("17. Back");


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
                        EditCourseDescription(course);
                        break;

                    case "4":
                        ShowModules(course);
                        break;

                    case "5":
                        AddModule(course);
                        break;

                    case "6":
                        AddModuleContent(course);
                        break;

                    case "7":
                        EditModule(course);
                        break;

                    case "8":
                        RemoveModuleContent(course);
                        break;

                    case "9":
                        AddAssignment(course);
                        break;

                    case "10":
                        EditAssignment(course);
                        break;

                    case "11":
                        ShowAssignments(course);
                        break;

                    case "12":
                        ManageAssignmentGroups(course);
                        break;

                    case "13":
                        RemoveAssignment(course);
                        break;

                    case "14"://Issue #7 start: Grading submissions
                        GradeSubmission(course);
                        break;

                    case "15":
                        ShowRoster(course);
                        break;

                    case "16":
                        ShowSchedule(course);
                        break;

                    case "17":
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
                newAssignment.Id = 1;
            }
            else
            {
                newAssignment.Id = course.Assignments.Max(a => a.Id) + 1;
            }

            course.Assignments.Add(newAssignment);

            Console.WriteLine($"Assignment '{newAssignment.Name}' added!");
        }

        public void EditAssignment(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available to edit.");
                return;
            }

            Console.WriteLine("\nAssignments:");
            foreach (var a in course.Assignments)
            {
                Console.WriteLine($"{a.Id} - {a.Name}");
            }

            Console.Write("\nEnter Assignment Id to edit: ");
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


            Console.Write($"New Name (Current: {assignment.Name}): ");
            var newName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
                assignment.Name = newName;
            }

            Console.Write($"New Description (current: {assignment.Description}): ");
            var newDesc = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newDesc))
            {
                assignment.Description = newDesc;
            }

            Console.Write($"New Due Date (current: {assignment.DueDate}) (MM/DD/YYYY): ");
            var dateInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(dateInput))
            {
                if (DateTime.TryParse(dateInput, out DateTime newDate))
                {
                    assignment.DueDate = newDate;
                }
                else
                {
                    Console.WriteLine("Invalid date. Keeping old due date.");
                }
            }

            Console.WriteLine("Assignment updated successfully!");
        }

        //Issue #8: Deleting an assignment/submission by ID
        public void RemoveAssignment(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available to delete.");
                return;
            }

            Console.WriteLine("\nAssignments:");
            foreach (var a in course.Assignments)
            {
                Console.WriteLine($"{a.Id} - {a.Name}");
            }

            Console.Write("\nEnter Assignment Id to delete: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int assignmentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var assignment = course.Assignments
                .FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            //Confirm delete
            Console.Write($"Are you sure you want to delete '{assignment.Name}'? (Y/N): ");
            var confirm = Console.ReadLine()?.Trim() ?? "";

            if (!confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            course.Assignments.Remove(assignment);

            Console.WriteLine("Assignment and all submissions deleted.");
        }
        //Problem when student instance submits multiple submissioms? Grade doesn't reflect changes?
        public void GradeSubmission(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available.");
                return;
            }

            //Select assignment
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

            var assignment = course.Assignments
                .FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            if (!assignment.Submissions.Any())
            {
                Console.WriteLine("No submissions for this assignment.");
                return;
            }

            //Show submissions
            Console.WriteLine("\nSubmissions:");
            foreach (var s in assignment.Submissions)
            {
                Console.WriteLine($"{s.Id} - StudentId: {s.StudentId} | Submitted: {s.SubmissionDate}");
            }

            Console.Write("\nEnter Submission Id: ");
            var subId = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(subId, out int submissionId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var submission = assignment.Submissions
                .FirstOrDefault(s => s.Id == submissionId);

            if (submission == null)
            {
                Console.WriteLine("Submission not found.");
                return;
            }

            //Review content
            Console.WriteLine($"\nContent:\n{submission.Content}");

            //Assign grade
            Console.Write("\nEnter grade (Input a whole number, not a letter grade): ");
            var gradeInput = Console.ReadLine()?.Trim() ?? "";

            //If I want doubles change this
            if (!int.TryParse(gradeInput, out int grade))
            {
                Console.WriteLine("Invalid grade.");
                return;
            }

            submission.Grade = grade;

            Console.WriteLine("Submission graded successfully!");
        }

        //Issue #10: Add modules to course
        public void AddModule(Course course)
        {
            var module = new Module();

            Console.Write("Module Name: ");
            module.Name = Console.ReadLine()?.Trim() ?? "";

            bool addingContent = true;

            //Each entry adds content as a string to a list of strings
            while (addingContent)
            {
                //COntent treated as pages
                Console.Write("Enter module content (or type N to stop): ");
                var input = Console.ReadLine()?.Trim() ?? "";

                if (input.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    addingContent = false;
                }
                else if (!string.IsNullOrEmpty(input))
                {
                    var page = new PageItem
                    {
                        Title = "Page",
                        Content = input
                    };

                    module.Contents.Add(page);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            module.Id = course.Modules.Any() ? course.Modules.Max(m => m.Id) + 1 : 1;
            course.Modules.Add(module);

            Console.WriteLine($"Module {module.Name} added to course {course.Name}!");
        }
        //Issue #12: Edit module content. Needs an add module content butto for modules made with no content
        public void EditModule(Course course)
        {
            if (!course.Modules.Any())
            {
                Console.WriteLine("No modules available.");
                return;
            }

            Console.WriteLine("\nModules:");
            foreach (var m in course.Modules)
            {
                Console.WriteLine($"{m.Id} - {m.Name}");
            }

            Console.Write("\nSelect Module Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int moduleId))
            {
                Console.WriteLine("Invalid module Id.");
                return;
            }

            var module = course.Modules.FirstOrDefault(m => m.Id == moduleId);

            if (module == null)
            {
                Console.WriteLine("Module not found.");
                return;
            }

            if (!module.Contents.Any())
            {
                Console.WriteLine("No content to edit.");
                return;
            }

            Console.WriteLine("\nModule Content:");
            for (int i = 0; i < module.Contents.Count; i++)
            {
                Console.WriteLine($"{i}. {module.Contents[i]}");
            }

            Console.Write("\nSelect content number to edit: ");
            var contentInput = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(contentInput, out int index) || index < 0 || index >= module.Contents.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.WriteLine($"\nCurrent: {module.Contents[index]}");
            Console.Write("Enter new content: ");
            var newContent = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(newContent))
            {
                Console.WriteLine("Content cannot be empty.");
                return;
            }

            var item = module.Contents[index];

            if (item is PageItem page)
            {
                page.Content = newContent;
            }
            else if (item is FileItem file)
            {
                file.FilePath = newContent;
            }
            else if (item is AssignmentItem assignmentItem)
            {
                if (assignmentItem.Assignment != null)
                {
                    assignmentItem.Assignment.Description = newContent;
                }
            }

            Console.WriteLine("Module content updated successfully!");
        }
        //Issue #13: Remove module content, not entire module

        public void RemoveModuleContent(Course course)
        {
            if (!course.Modules.Any())
            {
                Console.WriteLine("No modules available.");
                return;
            }

            Console.WriteLine("\nModules:");
            foreach (var m in course.Modules)
            {
                Console.WriteLine($"{m.Id} - {m.Name}");
            }

            Console.Write("\nSelect Module Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int moduleId))
            {
                Console.WriteLine("Invalid module Id.");
                return;
            }

            var module = course.Modules.FirstOrDefault(m => m.Id == moduleId);

            if (module == null)
            {
                Console.WriteLine("Module not found.");
                return;
            }

            if (!module.Contents.Any())
            {
                Console.WriteLine("No content to remove.");
                return;
            }

            Console.WriteLine("\nModule Content:");
            for (int i = 0; i < module.Contents.Count; i++)
            {
                Console.WriteLine($"{i}. {module.Contents[i]}");
            }

            Console.Write("\nSelect content number to remove: ");
            var choice = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(choice, out int index) ||
                index < 0 ||
                index >= module.Contents.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var removed = module.Contents[index];
            module.Contents.RemoveAt(index);

            Console.WriteLine($"Removed content: {removed}");
        }
        //#14: Remove course

        public void DeleteCourse()
        {
            var courses = CourseServiceProxy.Current.Courses;

            if (!courses.Any())
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("\nCourses:");
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Id} - {c.Name} {c.Code}");
            }

            Console.Write("\nEnter Course Id to delete: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int courseId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var course = courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            courses.Remove(course);

            Console.WriteLine("Course deleted successfully.");

        }

        //Issue #15: Edit course description
        public void EditCourseDescription(Course course)
        {
            Console.WriteLine($"\nCurrent Description: {course.Description}");

            Console.Write("Enter new description: ");
            var newDescription = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(newDescription))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }

            course.Description = newDescription;

            Console.WriteLine("Course description updated successfully!");
        }

        //Adds for #21
        public void AddModuleContent(Course course)
        {
            if (!course.Modules.Any())
            {
                Console.WriteLine("No modules available.");
                return;
            }

            Console.WriteLine("\nModules:");
            foreach (var m in course.Modules)
            {
                Console.WriteLine($"{m.Id} - {m.Name}");
            }

            Console.Write("Select Module Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int moduleId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var module = course.Modules.FirstOrDefault(m => m.Id == moduleId);

            if (module == null)
            {
                Console.WriteLine("Module not found.");
                return;
            }

            Console.WriteLine("\n1. Add Page");
            Console.WriteLine("2. Add File");
            Console.WriteLine("3. Add Assignment");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddPage(module);
                    break;
                case "2":
                    AddFile(module);
                    break;
                case "3":
                    AddAssignmentToModule(course, module);
                    break;
            }
        }
        private void AddPage(Module module)
        {
            var page = new PageItem();

            Console.Write("Title: ");
            page.Title = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Content: ");
            page.Content = Console.ReadLine()?.Trim() ?? "";

            page.Id = module.Contents.Any() ? module.Contents.Max(c => c.Id) + 1 : 1;

            module.Contents.Add(page);

            Console.WriteLine("Page added!");
        }
        private void AddFile(Module module)
        {
            var file = new FileItem();

            Console.Write("File name: ");
            file.Title = Console.ReadLine()?.Trim() ?? "";

            Console.Write("File path: ");
            file.FilePath = Console.ReadLine()?.Trim() ?? "";

            file.Id = module.Contents.Any() ? module.Contents.Max(c => c.Id) + 1 : 1;

            module.Contents.Add(file);

            Console.WriteLine("File added!");
        }
        private void AddAssignmentToModule(Course course, Module module)
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

            Console.Write("Enter Assignment Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var assignment = course.Assignments.FirstOrDefault(a => a.Id == id);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            var item = new AssignmentItem
            {
                Id = module.Contents.Any() ? module.Contents.Max(c => c.Id) + 1 : 1,
                Title = assignment.Name,
                Assignment = assignment
            };

            module.Contents.Add(item);

            Console.WriteLine("Assignment added to module!");
        }

        public void ManageAssignmentGroups(Course course)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nAssignment Group Menu:");
                Console.WriteLine("1. Add Group");
                Console.WriteLine("2. Edit Group");
                Console.WriteLine("3. Delete Group");
                Console.WriteLine("4. List Groups");
                Console.WriteLine("5. Add Assignment to Group");
                Console.WriteLine("6. Back");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGroup(course);
                        break;
                    case "2":
                        EditGroup(course);
                        break;
                    case "3":
                        DeleteGroup(course);
                        break;
                    case "4":
                        ListGroups(course);
                        break;
                    case "5":
                        AddAssignmentToGroup(course);
                        break;
                    case "6":
                        running = false;
                        break;
                }
            }
        }
        private void AddGroup(Course course)
        {
            var group = new AssignmentGroup();

            Console.Write("Group Name: ");
            group.Name = Console.ReadLine()?.Trim() ?? "";

            group.Id = course.AssignmentGroups.Any()
                ? course.AssignmentGroups.Max(g => g.Id) + 1
                : 1;

            course.AssignmentGroups.Add(group);

            Console.WriteLine("Group added!");
        }
        private void ListGroups(Course course)
        {
            if (!course.AssignmentGroups.Any())
            {
                Console.WriteLine("No groups available.");
                return;
            }

            foreach (var g in course.AssignmentGroups)
            {
                Console.WriteLine($"{g.Id} - {g.Name} ({g.Assignments.Count} assignments)");
            }
        }
        private void EditGroup(Course course)
        {
            ListGroups(course);

            Console.Write("Enter Group Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int id)) return;

            var group = course.AssignmentGroups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            Console.Write("New Name: ");
            group.Name = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine("Group updated!");
        }
        private void DeleteGroup(Course course)
        {
            ListGroups(course);

            Console.Write("Enter Group Id to delete: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int id)) return;

            var group = course.AssignmentGroups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            course.AssignmentGroups.Remove(group);

            Console.WriteLine("Group deleted!");
        }
        private void AddAssignmentToGroup(Course course)
        {
            if (!course.Assignments.Any())
            {
                Console.WriteLine("No assignments available.");
                return;
            }

            ListGroups(course);

            Console.Write("Select Group Id: ");
            var groupInput = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(groupInput, out int groupId)) return;

            var group = course.AssignmentGroups.FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            Console.WriteLine("\nAssignments:");
            foreach (var a in course.Assignments)
            {
                Console.WriteLine($"{a.Id} - {a.Name}");
            }

            Console.Write("Select Assignment Id: ");
            var input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int assignmentId)) return;

            var assignment = course.Assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            if (group.Assignments.Any(a => a.Id == assignment.Id))
            {
                Console.WriteLine("Assignment already in group.");
                return;
            }

            group.Assignments.Add(assignment);

            Console.WriteLine("Assignment added to group!");
        }
    }


}
