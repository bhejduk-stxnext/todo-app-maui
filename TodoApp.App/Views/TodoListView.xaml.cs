using CommunityToolkit.Mvvm.Messaging;
using TodoApp.ViewModels;
using TodoApp.ViewModels.Messages;

namespace TodoApp.App.Views;

public partial class TodoListView : BaseContentView<TodoListViewModel>
{
    public TodoListView(TodoListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
        RegisterMessages();
    }

    public TodoListView()
    {
        InitializeComponent();
        RegisterMessages();
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<TodoItemUpdatedMessage>(
            this,
            async void (r, m) =>
            {
                var vm = ViewModel.TodoItems.First(x => x.TodoItem.Id == m.Value.Id);

                var sorted = await Task.Run(() =>
                {
                    vm.Title = m.Value.Title;
                    vm.DueDate = m.Value.DueDate;
                    vm.Important = m.Value.Important;
                    vm.IsCompleted = m.Value.Completed;

                    var sorted = ViewModel
                        .GetSorted()
                        .ToList();

                    return sorted;
                });

                var oldIndex = ViewModel.TodoItems.IndexOf(vm);
                var newIndex = sorted.IndexOf(vm);
#if WINDOWS
                // https://github.com/dotnet/maui/issues/17369#issuecomment-1830919911
                ViewModel.TodoItems.RemoveAt(oldIndex);
                ViewModel.TodoItems.Insert(newIndex, vm);
#else
                ViewModel.TodoItems.Move(oldIndex, newIndex);
#endif
            });
    }

    private void SelectableItemsView_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
            collectionView.SelectedItem = null;
    }
}