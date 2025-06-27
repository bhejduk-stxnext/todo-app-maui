using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using TodoApp.App.Pages;
using TodoApp.App.Views;
using TodoApp.Core;
using TodoApp.ViewModels;
using TodoApp.ViewModels.Factories;
using TodoApp.ViewModels.Pages;
using INavigation = TodoApp.ViewModels.Navigation.INavigation;

namespace TodoApp.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
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

        DatePickerHandler.Mapper.AppendToMapping(
            "Placeholder",
            (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

        EntryHandler.Mapper.AppendToMapping(
            "Placeholder",
            (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        builder.Services.AddSingleton<ITodoItemsService, TodoItemsService>();
        builder.Services.AddSingleton<INavigation, Navigation>();
        builder.Services.AddSingleton<ITodoListItemSummaryViewModelFactory, TodoListItemSummaryViewModelFactory>();

        builder.Services.AddTransient<MainPage, MainPageViewModel>();
        builder.Services.AddTransient<TodoItemDetailsPage, TodoItemDetailsPageViewModel>();

        builder.Services.AddTransient<TodoListView, TodoListViewModel>();
        builder.Services.AddTransient<TodoItemDetailsView, TodoItemDetailsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}