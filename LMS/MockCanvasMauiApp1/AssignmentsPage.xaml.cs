using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class AssignmentsPage : ContentPage
{
    private Course _course;
    private Student _student;
    private Assignment _selectedAssignment;

    public AssignmentsPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;


        AssignmentsListView.ItemsSource = course.Assignments;

        if (_student != null)
        {
            AddBtn.IsVisible = false;
            EditBtn.IsVisible = false;
            DeleteBtn.IsVisible = false;
        }
    }
    private async void OnAssignmentTapped(object sender, ItemTappedEventArgs e)
    {
        var assignment = e.Item as Assignment;

        if (assignment == null)
            return;

        _selectedAssignment = assignment;

        if (_student != null)
        {
            await Navigation.PushAsync(new SubmissionPage(_course, assignment, _student));
        }
            AssignmentsListView.SelectedItem = null;
    }
    private void OnAssignmentTapped(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedAssignment = e.SelectedItem as Assignment;
    }
    private async void OnAddAssignmentClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Assignment", "Enter name:");
        if (string.IsNullOrWhiteSpace(name)) return;

        var description = await DisplayPromptAsync("Assignment", "Enter description:");
        var pointsInput = await DisplayPromptAsync("Points", "Enter available points:");

        int.TryParse(pointsInput, out int points);

        var assignment = new Assignment
        {
            Id = _course.Assignments.Any()
                ? _course.Assignments.Max(a => a.Id) + 1
                : 1,

            Name = name,
            Description = description,
            AvailablePoints = points,
            Submissions = new List<Submission>()
        };

        _course.Assignments.Add(assignment);

        Refresh();
    }
    private async void OnEditAssignmentClicked(object sender, EventArgs e)
    {
        if (_selectedAssignment == null)
        {
            await DisplayAlert("Error", "Select an assignment first.", "OK");
            return;
        }

        var name = await DisplayPromptAsync("Edit", "Name:", initialValue: _selectedAssignment.Name);
        var description = await DisplayPromptAsync("Edit", "Description:", initialValue: _selectedAssignment.Description);
        var pointsInput = await DisplayPromptAsync("Edit", "Points:", initialValue: _selectedAssignment.AvailablePoints.ToString());

        if (string.IsNullOrWhiteSpace(name)) return;

        int.TryParse(pointsInput, out int points);

        _selectedAssignment.Name = name;
        _selectedAssignment.Description = description;
        _selectedAssignment.AvailablePoints = points;

        Refresh();
    }
    private async void OnDeleteAssignmentClicked(object sender, EventArgs e)
    {
        if (_selectedAssignment == null)
        {
            await DisplayAlert("Error", "Select an assignment first.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Delete", "Delete this assignment?", "Yes", "No");

        if (!confirm) return;

        _selectedAssignment.Submissions.Clear();

        _course.Assignments.Remove(_selectedAssignment);

        _selectedAssignment = null;

        Refresh();
    }
    private void Refresh()
    {
        AssignmentsListView.ItemsSource = null;
        AssignmentsListView.ItemsSource = _course.Assignments;
    }
    private async void OnCopyAssignmentClicked(object sender, EventArgs e)
    {
        if (_selectedAssignment == null)
        {
            await DisplayAlert("Error", "Select an assignment first.", "OK");
            return;
        }

        var otherCourses = CourseServiceProxy.Current.Courses
            .Where(c => c.Id != _course.Id)
            .ToList();

        if (!otherCourses.Any())
        {
            await DisplayAlert("Error", "No other courses available.", "OK");
            return;
        }

        var courseNames = otherCourses.Select(c => c.Name).ToArray();

        var selectedName = await DisplayActionSheet(
            "Select destination course",
            "Cancel",
            null,
            courseNames);

        if (selectedName == "Cancel" || selectedName == null)
            return;

        var targetCourse = otherCourses
            .FirstOrDefault(c => c.Name == selectedName);

        if (targetCourse == null)
            return;

        var newAssignment = new Assignment
        {
            Id = targetCourse.Assignments.Any()
                ? targetCourse.Assignments.Max(a => a.Id) + 1
                : 1,

            Name = _selectedAssignment.Name,
            Description = _selectedAssignment.Description,
            DueDate = _selectedAssignment.DueDate,
            AvailablePoints = _selectedAssignment.AvailablePoints,

            Submissions = new List<Submission>()
        };

        targetCourse.Assignments.Add(newAssignment);

        await DisplayAlert("Success", "Assignment copied successfully!", "OK");
    }
    private async void OnAddQuizClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Quiz", "Enter quiz name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var question = await DisplayPromptAsync("Quiz", "Enter question:");

        var pointsInput = await DisplayPromptAsync("Quiz", "Enter points:");

        int.TryParse(pointsInput, out int points);

        var quiz = new QuizAssignment
        {
            Id = _course.Assignments.Any()
                ? _course.Assignments.Max(a => a.Id) + 1
                : 1,

            Name = name,
            Question = question,
            AvailablePoints = points,
            Submissions = new List<Submission>()
        };

        _course.Assignments.Add(quiz);

        Refresh();
    }
    private async void OnViewCommentsClicked(object sender, EventArgs e)
    {
        if (_selectedAssignment == null)
        {
            await DisplayAlert(
                "Error",
                "Select an assignment first.",
                "OK");

            return;
        }

        await Navigation.PushAsync(
            new AssignmentCommentsPage(_selectedAssignment));
    }
}