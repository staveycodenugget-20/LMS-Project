using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class SubmissionPage : ContentPage
{
    private Course _course;
    private Assignment _assignment;
    private Student _student;
    private string _selectedFilePath = "";
    private string _selectedFileName = "";

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

        CommentsListView.ItemsSource = _assignment.Comments;
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
                SubmissionDate = DateTime.Now,
                FilePath = _selectedFilePath,
                FileName = _selectedFileName
            };

            _assignment.Submissions.Add(submission);

            await DisplayAlert("Success", "Submission added.", "OK");
        }

        await Navigation.PopAsync();
    }
    private async void OnUploadFileClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync();

            if (result != null)
            {
                _selectedFilePath = result.FullPath;
                _selectedFileName = result.FileName;

                FileLabel.Text = $"Selected: {_selectedFileName}";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void OnPostCommentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CommentEditor.Text))
            return;

        var comment = new AssignmentComment
        {
            Id = _assignment.Comments.Any()
                ? _assignment.Comments.Max(c => c.Id) + 1
                : 1,

            StudentId = _student.Id,

            AssignmentId = _assignment.Id,
            //Bug where author of comment always shows teacher
            AuthorName = _student.Name,

            Message = CommentEditor.Text,

            DatePosted = DateTime.Now
        };

        _assignment.Comments.Add(comment);

        CommentEditor.Text = "";

        CommentsListView.ItemsSource = null;
        CommentsListView.ItemsSource = _assignment.Comments;

        await DisplayAlert("Success", "Comment posted.", "OK");
    }
}