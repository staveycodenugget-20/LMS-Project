using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class StudentCoursesPage : ContentPage
{
    private Student _student;

    public StudentCoursesPage(Student student)
    {
        InitializeComponent();

        _student = student;

        StudentNameLabel.Text = $"{student.Name}'s Courses";

        LoadCourses();
    }

    private void LoadCourses()
    {
        var courses = CourseServiceProxy.Current.Courses
            .Where(c => c.Roster.Any(s => s.Id == _student.Id))
            .ToList();

        CourseList.ItemsSource = courses;

    }
    private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
    {
        var course = e.CurrentSelection?.FirstOrDefault() as Course;

        if (course == null)
            return;

        await DisplayAlertAsync("Course Selected", course.Name, "OK");
    }
}