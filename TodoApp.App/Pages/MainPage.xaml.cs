using TodoApp.ViewModels.Pages;

namespace TodoApp.App.Pages;

public sealed partial class MainPage : BaseContentPage<MainPageViewModel>
{
    public MainPage(MainPageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}