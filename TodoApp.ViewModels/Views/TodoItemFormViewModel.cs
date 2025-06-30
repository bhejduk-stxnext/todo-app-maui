using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoItemFormViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private bool _completed;

    [ObservableProperty]
    private DateTimeOffset _createdDate;

    [ObservableProperty]
    [MaxLength(100)]
    [NotifyDataErrorInfo]
    private string _details = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _dueDate;

    [ObservableProperty]
    private bool _important;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Required")]
    [MinLength(2)]
    [MaxLength(100)]
    private string _title = string.Empty;

    public bool CanSave()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}