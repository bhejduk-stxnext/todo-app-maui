using TodoApp.ViewModels;

namespace TodoApp.App.Pages;

// ReSharper disable PartialTypeWithSinglePart
public partial class BaseContentPage<TViewModel> : ContentPage
    where TViewModel : BaseViewModel
{
    protected BaseContentPage(TViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
    }

    public TViewModel ViewModel { get; }
}