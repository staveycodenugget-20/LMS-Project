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
        var courses = CourseServiceProxy.Current.Courses;

        if (!courses.Any())
        {
            await DisplayAlert("Error", "No courses available.", "OK");
            return;
        }

        var courseNames = courses.Select(c => c.Name).ToArray();

        var selectedName = await DisplayActionSheet(
            "Select Course",
            "Cancel",
            null,
            courseNames);

        if (selectedName == "Cancel")
            return;

        var selectedCourse = courses.FirstOrDefault(c => c.Name == selectedName);

        if (selectedCourse == null)
            return;

        await Navigation.PushAsync(new ManageStudentsPage(selectedCourse));
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

    //private async void OnViewStudentsClicked(object sender, EventArgs e)
    //{
    //    var courses = CourseServiceProxy.Current.Courses;

    //    if (!courses.Any())
    //    {
    //        await DisplayAlert("Error", "No courses available.", "OK");
    //        return;
    //    }

    //    var courseNames = courses.Select(c => c.Name).ToArray();

    //    var selectedName = await DisplayActionSheet(
    //        "Select Course",
    //        "Cancel",
    //        null,
    //        courseNames);

    //    if (selectedName == "Cancel" || selectedName == null)
    //        return;

    //    var selectedCourse = courses.First(c => c.Name == selectedName);

    //    await Navigation.PushAsync(new TeacherCourseRosterPage(selectedCourse));
    //}
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

    private async void OnSemesterSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SemesterSettingsPage());
    }

}