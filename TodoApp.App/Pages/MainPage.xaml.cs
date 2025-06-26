using TodoApp.ViewModels;

namespace TodoApp.App.Pages;

public partial class MainPage : BaseContentPage<MainPageViewModel>
{
    public MainPage(MainPageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}