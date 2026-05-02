namespace MockCanvasMauiApp1;

public partial class TeacherPage : ContentPage
{
    public TeacherPage()
    {
        InitializeComponent();
    }

    private async void OnManageCoursesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageCoursesPage());
    }
    private async void OnManageStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageStudentsPage());
    }

    private async void OnAddAssignmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddAssignmentPage());
    }

    private async void OnViewStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeacherCourseRosterPage());
    }
}