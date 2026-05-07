using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class CourseSettingsPage : ContentPage
{
    private Course _course;

    public CourseSettingsPage(Course course)
    {
        InitializeComponent();

        _course = course;

        AEntry.Text = _course.GradeCutoffs["A"].ToString();
        BEntry.Text = _course.GradeCutoffs["B"].ToString();
        CEntry.Text = _course.GradeCutoffs["C"].ToString();
        DEntry.Text = _course.GradeCutoffs["D"].ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (double.TryParse(AEntry.Text, out double a) &&
            double.TryParse(BEntry.Text, out double b) &&
            double.TryParse(CEntry.Text, out double c) &&
            double.TryParse(DEntry.Text, out double d))
        {
            _course.GradeCutoffs["A"] = a;
            _course.GradeCutoffs["B"] = b;
            _course.GradeCutoffs["C"] = c;
            _course.GradeCutoffs["D"] = d;

            await DisplayAlert("Saved", "Grade settings updated", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Enter valid numbers", "OK");
        }
    }
}