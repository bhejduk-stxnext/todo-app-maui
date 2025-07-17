using TodoApp.ViewModels;

namespace TodoApp.App.Views;

// ReSharper disable PartialTypeWithSinglePart
public abstract partial class BaseContentView<TViewModel> : ContentView
    where TViewModel : BaseViewModel
{
    protected BaseContentView(TViewModel viewModel)
    {
        BindingContext = viewModel;
    }

    // needed when view is initialized inside XAML
    public BaseContentView() { }

    protected TViewModel ViewModel => (TViewModel)BindingContext;
}