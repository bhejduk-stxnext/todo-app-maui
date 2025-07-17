using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Core;
using TodoApp.Core.Services;
using TodoApp.ViewModels.Localization;
using TodoApp.ViewModels.Models;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoItemFormViewModel : BaseViewModel
{
    private readonly ITodoItemsService _todoItemsService;

    private bool _initialized;

    [ObservableProperty]
    public partial IReadOnlyList<Category> Categories { get; set; } = [];

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
    [MinLength(2)]
    [MaxLength(100)]
    public partial string Title { get; set; }

    [ObservableProperty]
    [MaxLength(100)]
    [NotifyDataErrorInfo]
    public partial string Details { get; set; }

    [ObservableProperty]
    public partial Category SelectedCategory { get; set; }

    [ObservableProperty]
    public partial DateTime CreatedDate { get; set; }

    [ObservableProperty]
    public partial DateTime? Deadline { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; }

    [ObservableProperty]
    public partial TimeSpan SelectedTime { get; set; }

    [ObservableProperty]
    public partial bool HasDeadline { get; set; }

    [ObservableProperty]
    public partial bool Important { get; set; }

    [ObservableProperty]
    public partial bool Completed { get; set; }

    public TodoItemFormViewModel(ITodoItemsService todoItemsService, ILocalization localization)
    {
        Title = string.Empty;
        Details = string.Empty;
        SelectedDate = DateTime.Now;

        _todoItemsService = todoItemsService;

        if (NoneCategory.Instance.Name == string.Empty)
            NoneCategory.Instance.Name = localization.GetString(Strings.NoneCategory);

        SelectedCategory = NoneCategory.Instance;
    }

    [RelayCommand]
    private async Task InitializeAsync(CancellationToken cancellation)
    {
        if (!_initialized)
        {
            await LoadItemsAsync(cancellation)
                .ConfigureAwait(false);
        }

        _initialized = true;
    }

    private async Task LoadItemsAsync(CancellationToken cancellation)
    {
        // setting categories resets category, so save it to temp
        var initialId = SelectedCategory.Id;

        var categories = await _todoItemsService.GetCategoriesAsync(cancellation);

        Categories = categories
            .Prepend(NoneCategory.Instance)
            .ToList();

        SelectedCategory = Categories.First(x => x.Id == initialId);
    }

    public bool CanSave()
    {
        ValidateAllProperties();
        return !HasErrors;
    }

    partial void OnSelectedDateChanged(DateTime value)
    {
        UpdateDeadline();
    }

    partial void OnSelectedTimeChanged(TimeSpan value)
    {
        UpdateDeadline();
    }

    private void UpdateDeadline()
    {
        if (HasDeadline)
            Deadline = SelectedDate.Date + SelectedTime;
    }

    [RelayCommand]
    private void ClearDeadline()
    {
        Deadline = null;
    }
}