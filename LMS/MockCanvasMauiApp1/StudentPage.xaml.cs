using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class StudentPage : ContentPage
{
    private Student _student;

    public StudentPage()
    {
        InitializeComponent();
    }

    public StudentPage(Student student)
    {
        InitializeComponent();
        _student = student;
    }

    private async void OnViewCoursesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentCoursesPage(_student));
    }
}