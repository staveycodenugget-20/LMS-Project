using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class AssignmentsPage : ContentPage
{
    private Course _course;
    private Student _student;

    public AssignmentsPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        AssignmentsListView.ItemsSource = course.Assignments;
    }
}