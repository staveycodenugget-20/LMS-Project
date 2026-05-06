using CLI.LMS.Services;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class StudentSelectionPage : ContentPage
{
    //BUG: omnly shows students from initial course
    public StudentSelectionPage()
    {
        InitializeComponent();

        StudentList.ItemsSource = StudentServiceProxy.Current.Students;
    }

    private async void OnStudentSelected(object sender, SelectionChangedEventArgs e)
    {
        var student = e.CurrentSelection.FirstOrDefault() as Student;

        if (student == null)
            return;

        await Navigation.PushAsync(new StudentCoursesPage(student));
    }
}