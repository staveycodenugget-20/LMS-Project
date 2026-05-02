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

        var courses = CourseServiceProxy.Current.Courses
            .Where(c => c.Roster.Any(s => s.Id == student.Id))
            .ToList();

        CoursesListView.ItemsSource = courses;
    }

    /*  private async void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
          var selectedCourse = e.SelectedItem as Course;

          if (selectedCourse == null)
              return;

          await DisplayAlert("Course Selected", selectedCourse.Name, "OK");
      }*/
    private async void OnCourseTapped(object sender, ItemTappedEventArgs e)
    {
        var selectedCourse = e.Item as Course;

        if (selectedCourse == null)
            return;

        CoursesListView.SelectedItem = null; // optional cleanup

        await Navigation.PushAsync(new CourseDetailsPage(selectedCourse, _student));
    }
}