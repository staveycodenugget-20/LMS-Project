using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ModulesPage : ContentPage
{
    private Course _course;
    private Student _student;

    public ModulesPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        ModulesListView.ItemsSource = course.Modules;
    }

    private async void OnModuleTapped(object sender, ItemTappedEventArgs e)
    {
        var module = e.Item as Module;

        if (module == null)
            return;

        ModulesListView.SelectedItem = null;

        await Navigation.PushAsync(new ModuleDetailsPage(module));
    }
}