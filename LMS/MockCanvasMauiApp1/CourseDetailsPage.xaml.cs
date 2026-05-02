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
    }

    private async void OnModulesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModulesPage(_course, _student));
    }

    private async void OnAssignmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AssignmentsPage(_course, _student));
    }

    private async void OnGradesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GradesPage(_course, _student));
    }
}