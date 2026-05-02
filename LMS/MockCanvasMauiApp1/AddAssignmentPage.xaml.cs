using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class AddAssignmentPage : ContentPage
{
    private Course _course;

    public AddAssignmentPage()
    {
        InitializeComponent();

        _course = CourseServiceProxy.Current.Courses.First();
    }

    private void OnCreateAssignmentClicked(object sender, EventArgs e)
    {
        var assignment = new Assignment
        {
            Id = _course.Assignments.Any()
                ? _course.Assignments.Max(a => a.Id) + 1
                : 1,

            Name = NameEntry.Text,
            Description = DescriptionEditor.Text,
            AvailablePoints = int.TryParse(PointsEntry.Text, out int pts) ? pts : 100
        };

        _course.Assignments.Add(assignment);

        DisplayAlert("Success", "Assignment created!", "OK");

        NameEntry.Text = "";
        DescriptionEditor.Text = "";
        PointsEntry.Text = "";
    }
}