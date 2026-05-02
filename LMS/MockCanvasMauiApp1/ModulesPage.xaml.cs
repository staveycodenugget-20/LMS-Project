using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ModulesPage : ContentPage
{
    private Course _course;
    private Student _student;
    private Module _selectedModule;

    public ModulesPage(Course course, Student student)
    {
        InitializeComponent();

        _course = course;
        _student = student;

        ModulesListView.ItemsSource = course.Modules;

        if (_student != null)
        {
            AddModuleBtn.IsVisible = false;
            EditModuleBtn.IsVisible = false;
            DeleteModuleBtn.IsVisible = false;
            ManageContentBtn.IsVisible = false;
        }
    }

    private async void OnModuleTapped(object sender, ItemTappedEventArgs e)
    {
        var module = e.Item as Module;

        if (module == null)
            return;

        _selectedModule = module;

        ModulesListView.SelectedItem = null;

        if (_student != null)
        {
            await Navigation.PushAsync(new ModuleDetailsPage(_course, module));
        }
    }
    private async void OnAddModuleClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Module", "Enter module name:");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var module = new Module
        {
            Id = _course.Modules.Any()
                ? _course.Modules.Max(m => m.Id) + 1
                : 1,
            Name = name,
            Contents = new List<ModuleContent>()
        };

        _course.Modules.Add(module);

        Refresh();
    }
    private async void OnEditModuleClicked(object sender, EventArgs e)
    {
        if (_selectedModule == null)
        {
            await DisplayAlert("Error", "Select a module first.", "OK");
            return;
        }

        var name = await DisplayPromptAsync("Edit Module", "Name:", _selectedModule.Name);

        if (string.IsNullOrWhiteSpace(name))
            return;

        _selectedModule.Name = name;

        Refresh();
    }
    private async void OnDeleteModuleClicked(object sender, EventArgs e)
    {
        if (_selectedModule == null)
        {
            await DisplayAlert("Error", "Select a module first.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Delete", "Delete this module?", "Yes", "No");

        if (!confirm) return;

        _selectedModule.Contents.Clear();

        _course.Modules.Remove(_selectedModule);

        _selectedModule = null;

        Refresh();
    }
    private async void OnManageContentClicked(object sender, EventArgs e)
    {
        if (_selectedModule == null)
        {
            await DisplayAlert("Error", "Select a module first.", "OK");
            return;
        }

        await Navigation.PushAsync(new ModuleDetailsPage(_course, _selectedModule));
    }
    private void Refresh()
    {
        ModulesListView.ItemsSource = null;
        ModulesListView.ItemsSource = _course.Modules;
    }
}