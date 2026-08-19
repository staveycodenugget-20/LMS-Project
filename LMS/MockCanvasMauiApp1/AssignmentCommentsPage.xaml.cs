using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class AssignmentCommentsPage : ContentPage
{
    private Assignment _assignment;
    private Teacher _teacher;

    public AssignmentCommentsPage(Assignment assignment)
	{
		InitializeComponent();

        _assignment = assignment;

        AssignmentNameLabel.Text = assignment.Name;

        CommentsListView.ItemsSource = _assignment.Comments;
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

            AssignmentId = _assignment.Id,

            StudentId = 0,

            AuthorName = "Teacher",

            Message = CommentEditor.Text,

            DatePosted = DateTime.Now
        };

        _assignment.Comments.Add(comment);

        CommentEditor.Text = "";

        CommentsListView.ItemsSource = null;
        CommentsListView.ItemsSource = _assignment.Comments;

        await DisplayAlert(
            "Success",
            "Comment posted.",
            "OK");
    }
}