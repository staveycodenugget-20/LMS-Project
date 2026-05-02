using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ModuleDetailsPage : ContentPage
{
    private Course _course;
    private Module _module;
    private ModuleContent _selectedContent;

    public ModuleDetailsPage(Course course, Module module)
    {
        InitializeComponent();

        _course = course;
        _module = module;

        ContentListView.ItemsSource = _module.Contents;
    }
    private void OnContentTapped(object sender, ItemTappedEventArgs e)
    {
        _selectedContent = e.Item as ModuleContent;
        ContentListView.SelectedItem = null;
    }
    private async void OnAddContent(object sender, EventArgs e)
    {
        var title = await DisplayPromptAsync("Content", "Enter title:");
        var body = await DisplayPromptAsync("Content", "Enter body:");

        if (string.IsNullOrWhiteSpace(title)) return;

        var content = new TextModuleContent
        {
            Id = _module.Contents.Any() ? _module.Contents.Max(c => c.Id) + 1 : 1,
            Title = title,
            Body = body
        };

        _module.Contents.Add(content);

        Refresh();
    }
    private async void OnEditContent(object sender, EventArgs e)
    {
        if (_selectedContent == null)
        {
            await DisplayAlert("Error", "Select content first", "OK");
            return;
        }

        _selectedContent.Title =
            await DisplayPromptAsync("Edit", "Title:", initialValue: _selectedContent.Title);

        if (_selectedContent is TextModuleContent textContent)
        {
            textContent.Body =
                await DisplayPromptAsync("Edit", "Body:", initialValue: textContent.Body);
        }

        Refresh();
    }
    private async void OnDeleteContent(object sender, EventArgs e)
    {
        if (_selectedContent == null)
        {
            await DisplayAlert("Error", "Select content first", "OK");
            return;
        }

        _module.Contents.Remove(_selectedContent);
        _selectedContent = null;

        Refresh();
    }
    private void Refresh()
    {
        ContentListView.ItemsSource = null;
        ContentListView.ItemsSource = _module.Contents;
    }

}
