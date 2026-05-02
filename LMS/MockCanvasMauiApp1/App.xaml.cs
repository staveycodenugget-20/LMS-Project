using CLI.LMS.Services;
using Microsoft.Extensions.DependencyInjection;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SeedData();


        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private void SeedData()
        {
            var studentService = StudentServiceProxy.Current;
            var courseService = CourseServiceProxy.Current;

            if (!studentService.Students.Any())
            {
                var s1 = studentService.Add(new Student { Name = "Alice" });
                var s2 = studentService.Add(new Student { Name = "Bob" });

                var course = courseService.Add(new Course
                {
                    Name = "Intro to Programming",
                    Code = "COP1000",
                    Description = "Basic programming course"
                });

                course.Roster.Add(s1);
                course.Roster.Add(s2);

                var a1 = new Assignment
                {
                    Id = 1,
                    Name = "Homework 1",
                    Description = "Variables and Data Types",
                    AvailablePoints = 100,
                    DueDate = DateTime.Now.AddDays(7),
                    Submissions = new List<Submission>()
                };

                var a2 = new Assignment
                {
                    Id = 2,
                    Name = "Homework 2",
                    Description = "Control Structures",
                    AvailablePoints = 100,
                    DueDate = DateTime.Now.AddDays(10),
                    Submissions = new List<Submission>()
                };

                course.Assignments.Add(a1);
                course.Assignments.Add(a2);


                a1.Submissions.Add(new Submission
                {
                    Id = 1,
                    StudentId = s1.Id,
                    AssignmentId = a1.Id,
                    Content = "Alice HW1 submission",
                    SubmissionDate = DateTime.Now,
                    Grade = 95,
                    //Feedback = "Great job!"
                });

                a2.Submissions.Add(new Submission
                {
                    Id = 2,
                    StudentId = s1.Id,
                    AssignmentId = a2.Id,
                    Content = "Alice HW2 submission",
                    SubmissionDate = DateTime.Now,
                    Grade = 88,
                    //Feedback = "Good work, minor mistakes."
                });

                a1.Submissions.Add(new Submission
                {
                    Id = 3,
                    StudentId = s2.Id,
                    AssignmentId = a1.Id,
                    Content = "Bob HW1 submission",
                    SubmissionDate = DateTime.Now,
                    Grade = 72,
                    //Feedback = "Needs improvement."
                });

                a2.Submissions.Add(new Submission
                {
                    Id = 4,
                    StudentId = s2.Id,
                    AssignmentId = a2.Id,
                    Content = "Bob HW2 submission",
                    SubmissionDate = DateTime.Now,
                    Grade = 85,
                    //Feedback = "Better!"
                });

            }
        }
    }
}