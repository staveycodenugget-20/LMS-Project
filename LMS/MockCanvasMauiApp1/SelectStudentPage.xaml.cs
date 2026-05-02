using CLI.LMS.Services;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class SelectStudentPage : ContentPage
{
    public SelectStudentPage()
    {
        InitializeComponent();

        StudentListView.ItemsSource = StudentServiceProxy.Current.Students;
    }

 /*  private async void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedStudent = e.SelectedItem as Student;

        if (selectedStudent == null)
            return;

        await Navigation.PushAsync(new SelectStudentPage());
    }*/
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await DisplayAlert("Debug",
        $"Students count: {StudentServiceProxy.Current.Students?.Count}",
        "OK");

        StudentListView.ItemsSource = StudentServiceProxy.Current.Students;
    }
    private async void OnStudentTapped(object sender, ItemTappedEventArgs e)
    {
        var selectedStudent = e.Item as Student;

        if (selectedStudent == null)
            return;

        StudentListView.SelectedItem = null;

        await Navigation.PushAsync(new StudentMenuPage(selectedStudent));
    }
}