using UserInformation.Services;

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

    private async void OnManageAssignmentsClicked(object sender, EventArgs e)
    {
        var course = CourseServiceProxy.Current.Courses.FirstOrDefault();

        if (course == null)
        {
            await DisplayAlert("Error", "No courses exist.", "OK");
            return;
        }

        await Navigation.PushAsync(new AssignmentsPage(course, null));
    }

    private async void OnViewStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeacherCourseRosterPage());
    }
    private async void OnManageModulesClicked(object sender, EventArgs e)
    {
        var course = CourseServiceProxy.Current.Courses.FirstOrDefault();

        if (course == null)
        {
            await DisplayAlert("Error", "No courses available.", "OK");
            return;
        }

        await Navigation.PushAsync(new ModulesPage(course, null));
    }
}