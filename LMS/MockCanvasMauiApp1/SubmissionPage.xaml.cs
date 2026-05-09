using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class SubmissionPage : ContentPage
{
    private Course _course;
    private Assignment _assignment;
    private Student _student;

    public SubmissionPage(Course course, Assignment assignment, Student student)
    {
        InitializeComponent();

        _course = course;
        _assignment = assignment;
        _student = student;

        if (_assignment is QuizAssignment quiz)
        {
            QuestionLabel.Text = quiz.Question;
        }
        else
        {
            QuestionLabel.IsVisible = false;
        }

        AssignmentNameLabel.Text = assignment.Name;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var content = SubmissionEditor.Text;

        if (string.IsNullOrWhiteSpace(content))
        {
            await DisplayAlert("Error", "Submission cannot be empty.", "OK");
            return;
        }

        var existing = _assignment.Submissions
            .FirstOrDefault(s => s.StudentId == _student.Id);

        if (existing != null)
        {
            existing.Content = content;
            existing.SubmissionDate = DateTime.Now;

            await DisplayAlert("Updated", "Submission updated.", "OK");
        }
        else
        {
            var submission = new Submission
            {
                Id = _assignment.Submissions.Any()
                    ? _assignment.Submissions.Max(s => s.Id) + 1
                    : 1,

                StudentId = _student.Id,
                Content = content,
                SubmissionDate = DateTime.Now
            };

            _assignment.Submissions.Add(submission);

            await DisplayAlert("Success", "Submission added.", "OK");
        }

        await Navigation.PopAsync();
    }
}