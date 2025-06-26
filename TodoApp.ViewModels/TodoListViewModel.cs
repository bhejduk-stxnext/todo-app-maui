using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Dispatching;
using TodoApp.Core;
using TodoApp.ViewModels.Factories;

namespace TodoApp.ViewModels;

public partial class TodoListViewModel : BaseViewModel
{
    private readonly IDispatcher _dispatcher;
    private readonly IMessenger _messenger;
    private readonly ITodoItemsService _todoItemsService;
    private readonly ITodoListItemSummaryViewModelFactory _todoItemSummaryFactory;

    [ObservableProperty]
    private ObservableCollection<TodoListItemSummaryViewModel> _todoItems;

    public TodoListViewModel(
        ITodoItemsService todoItemsService,
        IDispatcher dispatcher,
        IMessenger messenger,
        ITodoListItemSummaryViewModelFactory todoItemSummaryFactory)
    {
        _todoItemsService = todoItemsService;
        _dispatcher = dispatcher;
        _messenger = messenger;
        _todoItemSummaryFactory = todoItemSummaryFactory;
        _todoItems = [];
    }

    [RelayCommand]
    private async Task LoadItemsAsync()
    {
        var items = await _todoItemsService
            .GetAsync()
            .ConfigureAwait(false);

        var orderedItems = items
            .OrderBy(x => x.Completed)
            .ThenBy(x => !x.Important)
            .ToList();
        
        foreach (var item in orderedItems)
        {
            var summaryViewModel = _todoItemSummaryFactory.Create(item);

            await _dispatcher.DispatchAsync(() => TodoItems.Add(summaryViewModel));
        }
    }
}