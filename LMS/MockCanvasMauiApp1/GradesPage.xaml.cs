using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class GradesPage : ContentPage
{
    private Course _course;
    private Student _student;

    public GradesPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        var grades = course.Assignments.Select(a => new
        {
            AssignmentName = a.Name,
            Grade = a.Submissions?
                .FirstOrDefault(s => s.StudentId == student.Id)?.Grade,
            Points = a.AvailablePoints
        }).ToList();

        GradesListView.ItemsSource = grades;
    }
}