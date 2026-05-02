using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ManageCoursesPage : ContentPage
{
    private Course _selectedCourse;

    public ManageCoursesPage()
    {
        InitializeComponent();

        CoursesListView.ItemsSource = CourseServiceProxy.Current.Courses;
    }

    private void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedCourse = e.SelectedItem as Course;
    }

    private async void OnCreateCourseClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Course", "Enter course name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var course = new Course
        {
            Id = CourseServiceProxy.Current.Courses.Any()
                ? CourseServiceProxy.Current.Courses.Max(c => c.Id) + 1
                : 1,
            Name = name,
            Roster = new List<Student>()
        };

        CourseServiceProxy.Current.Add(course);

        CoursesListView.ItemsSource = null;
        CoursesListView.ItemsSource = CourseServiceProxy.Current.Courses;
    }
}