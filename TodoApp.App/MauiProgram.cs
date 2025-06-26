using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TodoApp.App.Pages;
using TodoApp.App.Views;
using TodoApp.Core;
using TodoApp.ViewModels;
using TodoApp.ViewModels.Factories;
using INavigation = TodoApp.ViewModels.Navigation.INavigation;

namespace TodoApp.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        try
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            builder.Services.AddSingleton<ITodoItemsService, TodoItemsService>();
            builder.Services.AddSingleton<INavigation, Navigation>();
            builder.Services.AddSingleton<ITodoListItemSummaryViewModelFactory, TodoListItemSummaryViewModelFactory>();

            builder.Services.AddTransient<MainPage, MainPageViewModel>();
            builder.Services.AddTransient<TodoListView, TodoListViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}