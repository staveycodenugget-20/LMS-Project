using CLI.LMS.Services;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ManageStudentsPage : ContentPage
{
    private Course _course;
    private Student _selected;

    public ManageStudentsPage(Course course)
    {
        InitializeComponent();

        _course = course;

        StudentsListView.ItemsSource = _course.Roster;
    }

    private void OnStudentTapped(object sender, ItemTappedEventArgs e)
    {
        _selected = e.Item as Student;

        StudentsListView.SelectedItem = null; 
    }

    private async void OnAddStudent(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Student", "Enter name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var student = new Student
        {
            Id = _course.Roster.Any()
                ? _course.Roster.Max(s => s.Id) + 1
                : 1,
            Name = name
        };

        _course.Roster.Add(student);

        Refresh();
    }

    private async void OnEditStudent(object sender, EventArgs e)
    {
        if (_selected == null)
        {
            await DisplayAlert("Error", "Select a student first.", "OK");
            return;
        }

        var name = await DisplayPromptAsync("Edit", "New name:", initialValue: _selected.Name);

        if (string.IsNullOrWhiteSpace(name))
            return;

        _selected.Name = name;

        Refresh();
    }

    private async void OnRemoveStudent(object sender, EventArgs e)
    {
        if (_selected == null)
        {
            await DisplayAlert("Error", "Select a student first.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Delete", "Remove this student?", "Yes", "No");

        if (!confirm) return;

        _course.Roster.Remove(_selected);

        _selected = null;

        Refresh();
    }

    private void Refresh()
    {
        StudentsListView.ItemsSource = null;
        StudentsListView.ItemsSource = _course.Roster;
    }
}