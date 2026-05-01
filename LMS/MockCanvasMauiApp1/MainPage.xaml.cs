using CLI.LMS.UserSections;

namespace MockCanvasMauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnTeacherClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeacherPage());
        }

        private async void OnStudentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentSelectionPage());
        }
    }
}
