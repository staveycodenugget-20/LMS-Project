using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class StudentMenuPage : ContentPage
{
    private Student _student;

    public StudentMenuPage(Student student)
    {
        InitializeComponent();

        _student = student;
        StudentNameLabel.Text = $"Welcome, {student.Name}";
    }

    private async void OnViewCoursesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentCoursesPage(_student));
    }

    private async void OnViewGradesClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "Grades page coming next", "OK");
    }
}