using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Networking;
using TodoApp.ViewModels.Localization;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels;

public sealed class ViewModelContext
{
    public IDispatcher Dispatcher { get; }

    public IMessenger Messenger { get; }

    public INavigation Navigation { get; }

    public ILocalization Localization { get; }

    public IConnectivity Connectivity { get; }

    public ISnackbar Snackbar { get; }
    
    public TimeProvider TimeProvider { get; }

    public ViewModelContext(
        IDispatcher dispatcher,
        IMessenger messenger,
        INavigation navigation,
        ILocalization localization,
        ISnackbar snackbar,
        IConnectivity connectivity,
        TimeProvider timeProvider)
    {
        Dispatcher = dispatcher;
        Messenger = messenger;
        Navigation = navigation;
        Localization = localization;
        Snackbar = snackbar;
        Connectivity = connectivity;
        TimeProvider = timeProvider;
    }
}