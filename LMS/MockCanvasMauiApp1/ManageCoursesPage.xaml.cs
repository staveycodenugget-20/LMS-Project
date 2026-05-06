using CLI.LMS.Services;
using UserInformation.Services;
using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ManageCoursesPage : ContentPage
{
    private Course _selectedCourse;

    public ManageCoursesPage()
    {
        InitializeComponent();

        CoursesListView.ItemsSource = CourseServiceProxy.Current.Courses;
    }

    private async void OnCourseTapped(object sender, ItemTappedEventArgs e)
    {
        var course = e.Item as Course;

        if (course == null)
            return;

        _selectedCourse = course;

        CoursesListView.SelectedItem = null;

    }

    private async void OnCreateCourseClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Course", "Enter course name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var course = new Course
        {
            Id = CourseServiceProxy.Current.Courses.Any()
                ? CourseServiceProxy.Current.Courses.Max(c => c.Id) + 1
                : 1,
            Name = name,
            Roster = new List<Student>()
        };

        CourseServiceProxy.Current.Add(course);

        CoursesListView.ItemsSource = null;
        CoursesListView.ItemsSource = CourseServiceProxy.Current.Courses;
    }
    private async void OnExportRosterClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        var lines = new List<string>
    {
        "Id,Name"
    };

        foreach (var student in _selectedCourse.Roster)
        {
            lines.Add($"{student.Id},{student.Name}");
        }

        var path = Path.Combine(FileSystem.AppDataDirectory, "roster.csv");

        File.WriteAllLines(path, lines);

        /*await DisplayAlert("Exported", path, "OK");*/

        //test, remove later
        await DisplayAlert("Exported",
    $"Saved {_selectedCourse.Roster.Count} students to file",
    "OK");

        //remove later, just to verify file contents
        var contents = File.ReadAllText(path);
        await DisplayAlert("File Contents", contents, "OK");
    }
    private async void OnImportRosterClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        var path = Path.Combine(FileSystem.AppDataDirectory, "roster.csv");

        if (!File.Exists(path))
        {
            await DisplayAlert("Error", "No roster file found.", "OK");
            return;
        }

        var lines = File.ReadAllLines(path).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length < 2) continue;

            int id = int.Parse(parts[0]);
            string name = parts[1];

            if (_selectedCourse.Roster.Any(s => s.Name == name))
                continue;

            var student = StudentServiceProxy.Current.Students
                .FirstOrDefault(s => s.Name == name);

            if (student == null)
            {
                student = new Student
                {
                    Name = name
                };

                StudentServiceProxy.Current.Add(student);
            }

            if (!_selectedCourse.Roster.Any(s => s.Name == student.Name))
            {
                _selectedCourse.Roster.Add(student);
            }
        }

        await DisplayAlert("Done", "Roster imported safely.", "OK");
    }
    private async void OnManageRosterClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        await Navigation.PushAsync(new TeacherCourseRosterPage(_selectedCourse));
    }
    private async void OnAddAnnouncementClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        var text = await DisplayPromptAsync("Announcement", "Enter message:");

        if (string.IsNullOrWhiteSpace(text))
            return;

        var announcement = new Announcement
        {
            Id = _selectedCourse.Announcements.Any()
                ? _selectedCourse.Announcements.Max(a => a.Id) + 1
                : 1,
            Message = text,
            CreatedAt = DateTime.Now
        };

        _selectedCourse.Announcements.Insert(0, announcement);

        await DisplayAlert("Success", "Announcement added.", "OK");
    }
    private async void OnExportAssignmentsClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        var lines = new List<string>
    {
        "Name,Description,Points"
    };

        foreach (var a in _selectedCourse.Assignments)
        {
            lines.Add($"{a.Name},{a.Description},{a.AvailablePoints}");
        }

        var path = Path.Combine(FileSystem.AppDataDirectory, "assignments.csv");

        File.WriteAllLines(path, lines);

        await DisplayAlert("Exported",
            $"{_selectedCourse.Assignments.Count} assignments saved.",
            "OK");
    }
    private async void OnImportAssignmentsClicked(object sender, EventArgs e)
    {
        if (_selectedCourse == null)
        {
            await DisplayAlert("Error", "Select a course first.", "OK");
            return;
        }

        var path = Path.Combine(FileSystem.AppDataDirectory, "assignments.csv");

        if (!File.Exists(path))
        {
            await DisplayAlert("Error", "No file found.", "OK");
            return;
        }

        var lines = File.ReadAllLines(path).Skip(1);

        int added = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length < 3) continue;

            string name = parts[0];
            string description = parts[1];
            int.TryParse(parts[2], out int points);

            if (_selectedCourse.Assignments.Any(a => a.Name == name))
                continue;

            var assignment = new Assignment
            {
                Id = _selectedCourse.Assignments.Any()
                    ? _selectedCourse.Assignments.Max(a => a.Id) + 1
                    : 1,

                Name = name,
                Description = description,
                AvailablePoints = points,

                Submissions = new List<Submission>()
            };

            _selectedCourse.Assignments.Add(assignment);
            added++;
        }

        await DisplayAlert("Import Complete",
            $"{added} assignment(s) added.",
            "OK");
    }
}