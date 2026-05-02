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
    private async void OnAssignmentTapped(object sender, ItemTappedEventArgs e)
    {
        var assignment = e.Item as Assignment;

        if (assignment == null)
            return;

        AssignmentsListView.SelectedItem = null;

        await Navigation.PushAsync(new AssignmentSubmissionPage(assignment, _student));
    }
}