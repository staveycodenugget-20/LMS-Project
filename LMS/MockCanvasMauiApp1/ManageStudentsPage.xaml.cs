using CLI.LMS.Services;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ManageStudentsPage : ContentPage
{
    private Student _selected;

    public ManageStudentsPage()
    {
        InitializeComponent();

        StudentsListView.ItemsSource = StudentServiceProxy.Current.Students;
    }

    private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selected = e.SelectedItem as Student;
    }

    private async void OnAddStudent(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Student", "Enter name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        StudentServiceProxy.Current.Add(new Student
        {
            Name = name
        });

        Refresh();
    }

    private async void OnEditStudent(object sender, EventArgs e)
    {
        if (_selected == null) return;

        var name = await DisplayPromptAsync("Edit Student", "Update name:", initialValue: _selected.Name);

        if (string.IsNullOrWhiteSpace(name)) return;

        _selected.Name = name;

        Refresh();
    }

    private void OnRemoveStudent(object sender, EventArgs e)
    {
        if (_selected == null) return;

        foreach (var course in CourseServiceProxy.Current.Courses)
        {
            course.Roster.RemoveAll(s => s.Id == _selected.Id);

            foreach (var assignment in course.Assignments)
            {
                assignment.Submissions.RemoveAll(sub => sub.StudentId == _selected.Id);
            }
        }

        StudentServiceProxy.Current.Remove(_selected);

        _selected = null;

        Refresh();
    }

    private void Refresh()
    {
        StudentsListView.ItemsSource = null;
        StudentsListView.ItemsSource = StudentServiceProxy.Current.Students;
    }
}