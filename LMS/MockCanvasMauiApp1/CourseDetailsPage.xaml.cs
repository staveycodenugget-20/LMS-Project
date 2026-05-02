using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class CourseDetailsPage : ContentPage
{
    private Course _course;
    private Student _student;

    public CourseDetailsPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        CourseNameLabel.Text = course.Name;

        AssignmentsListView.ItemsSource = course.Assignments;
    }
}