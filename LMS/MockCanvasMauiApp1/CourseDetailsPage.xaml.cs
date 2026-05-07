using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class CourseDetailsPage : ContentPage
{
    private Course _course;
    private Student _student;
    private Student _selectedStudent;

    public CourseDetailsPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        CourseNameLabel.Text = course.Name;

        RosterListView.ItemsSource = _course.Roster;

        double finalPercent = CalculateFinalGrade(_course, _student);
        string letter = GetLetterGrade(finalPercent);

        FinalGradeLabel.Text = $"Grade: {letter} ({finalPercent:F2}%)";

        AnnouncementsListView.ItemsSource = _course.Announcements;
    }

    private async void OnModulesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModulesPage(_course, _student));
    }

    private async void OnAssignmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AssignmentsPage(_course, _student));
    }

    private async void OnGradesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GradesPage(_course, _student));
    }
    private string GetLetterGrade(double percent)
    {
        var cutoffs = _course.GradeCutoffs;

        if (percent >= cutoffs["A"]) return "A";
        if (percent >= cutoffs["B"]) return "B";
        if (percent >= cutoffs["C"]) return "C";
        if (percent >= cutoffs["D"]) return "D";
        return "F";
    }
    private double CalculateFinalGrade(Course course, Student student)
    {
        if (!course.AssignmentGroups.Any())
        {
            double totalScore = 0;
            double totalPoints = 0;

            foreach (var assignment in course.Assignments)
            {
                var submission = assignment.Submissions?
                    .FirstOrDefault(s => s.StudentId == student.Id);

                if (submission != null)
                {
                    totalScore += submission.Grade ?? 0;
                    totalPoints += assignment.AvailablePoints;
                }
            }

            return totalPoints > 0 ? (totalScore / totalPoints) * 100 : 0;
        }

        double total = 0;

        foreach (var group in course.AssignmentGroups)
        {
            double groupScore = 0;
            double groupTotal = 0;

            foreach (var assignment in group.Assignments)
            {
                var submission = assignment.Submissions?
                    .FirstOrDefault(s => s.StudentId == student.Id);

                if (submission != null)
                {
                    groupScore += submission.Grade ?? 0;
                    groupTotal += assignment.AvailablePoints;
                }
            }

            if (groupTotal > 0)
            {
                double groupAverage = (groupScore / groupTotal) * 100;
                total += groupAverage * (group.Weight / 100);
            }
        }

        return total;
    }
    private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedStudent = e.SelectedItem as Student;
    }
    
}