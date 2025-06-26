using TodoApp.ViewModels;

namespace TodoApp.App.Pages;

public class BaseContentPage<TViewModel> : ContentPage
    where TViewModel : BaseViewModel
{
    protected BaseContentPage(TViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
    }

    public TViewModel ViewModel { get; }
}