using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class AssignmentSubmissionPage : ContentPage
{
    private Assignment _assignment;
    private Student _student;

    public AssignmentSubmissionPage(Assignment assignment, Student student)
    {
        InitializeComponent();

        _assignment = assignment;
        _student = student;

        AssignmentNameLabel.Text = assignment.Name;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var text = SubmissionEditor.Text?.Trim();

        if (string.IsNullOrEmpty(text))
        {
            await DisplayAlert("Error", "Submission cannot be empty.", "OK");
            return;
        }

        var existing = _assignment.Submissions?
            .FirstOrDefault(s => s.StudentId == _student.Id);

        if (existing != null)
        {
            existing.Content = text;
            existing.SubmissionDate = DateTime.Now;

            await DisplayAlert("Updated", "Submission updated!", "OK");
        }
        else
        {
            var newSubmission = new Submission
            {
                Id = _assignment.Submissions.Any()
                    ? _assignment.Submissions.Max(s => s.Id) + 1
                    : 1,

                StudentId = _student.Id,
                AssignmentId = _assignment.Id,
                Content = text,
                SubmissionDate = DateTime.Now
            };

            _assignment.Submissions.Add(newSubmission);

            await DisplayAlert("Success", "Submission submitted!", "OK");
        }

        await Navigation.PopAsync(); 
    }
}