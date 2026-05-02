using UserInformation.UserModels;

namespace MockCanvasMauiApp1;

public partial class ModuleDetailsPage : ContentPage
{
    private Module _module;

    public ModuleDetailsPage(Module module)
    {
        InitializeComponent();

        _module = module;

        ContentListView.ItemsSource = module.Contents;
    }
}