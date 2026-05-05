using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class TeacherCourseRosterPage : ContentPage
{
    private Course _course;
    private Student _selectedStudent;

    public TeacherCourseRosterPage(Course course)
    {
        InitializeComponent();

        _course = course;

        RosterListView.ItemsSource = _course.Roster;

    }

    private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedStudent = e.SelectedItem as Student;
    }

    private async void OnAddStudentClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Add Student", "Enter student name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var student = new Student
        {
            Id = _course.Roster.Any() ? _course.Roster.Max(s => s.Id) + 1 : 1,
            Name = name
        };

        _course.Roster.Add(student);

        RefreshRoster();
    }

    private void OnRemoveStudentClicked(object sender, EventArgs e)
    {
        if (_selectedStudent == null)
            return;

        _course.Roster.Remove(_selectedStudent);

        _selectedStudent = null;

        RefreshRoster();
    }

    private void RefreshRoster()
    {
        RosterListView.ItemsSource = null;
        RosterListView.ItemsSource = _course.Roster;
    }
}