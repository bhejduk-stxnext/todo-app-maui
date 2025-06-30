using TodoApp.ViewModels.Views;

namespace TodoApp.App.Views;

public partial class TodoListView : BaseContentView<TodoListViewModel>
{
    public TodoListView(TodoListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public TodoListView()
    {
        InitializeComponent();
    }

    private void SelectableItemsView_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // problem with unselecting items https://github.com/dotnet/maui/issues/10025
            collectionView.SelectedItem = null;
        }
    }
}