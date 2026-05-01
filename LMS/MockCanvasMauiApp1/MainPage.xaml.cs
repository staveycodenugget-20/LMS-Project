using CLI.LMS.UserSections;

namespace MockCanvasMauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTeacherClicked(object sender, EventArgs e)
        {
            var teacherMenu = new TeacherMenuSection();
            teacherMenu.EnterMainMenu();
        }

        private void OnStudentClicked(object sender, EventArgs e)
        {
            var studentMenu = new StudentMenuSection();
            studentMenu.EnterMainMenu();
        }
    }
}
