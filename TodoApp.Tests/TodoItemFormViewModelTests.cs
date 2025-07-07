using NSubstitute;
using TodoApp.Core;
using TodoApp.ViewModels.Localization;
using TodoApp.ViewModels.Models;
using TodoApp.ViewModels.Views;

namespace TodoApp.Tests;

public class TodoItemFormViewModelTests
{
    private readonly ITodoItemsService _todoItemsService = Substitute.For<ITodoItemsService>();
    private readonly ILocalization _localization = Substitute.For<ILocalization>();

    [Test]
    public async Task Constructor_SetsSelectedCategoryToNone()
    {
        // Arrange

        // Act
        var vm = CreateViewModel();

        // Assert
        await Assert
            .That(vm.SelectedCategory)
            .IsEqualTo(NoneCategory.Instance);
    }

    [Test]
    [Arguments("", true)]
    [Arguments("A", true)]
    [Arguments("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", true)] // 101 chars
    [Arguments("Valid Title", false)]
    public async Task Title_Validation_RequiredAndLength(string title, bool expectHasErrors)
    {
        // Arrange
        var vm = CreateViewModel();

        // Act
        vm.Title = title;

        // Assert
        await Assert
            .That(vm.HasErrors)
            .IsEqualTo(expectHasErrors);
    }

    [Test]
    [Arguments("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", true)] // 101 chars
    [Arguments("Some details", false)]
    public async Task Details_Validation_MaxLength(string details, bool expectHasErrors)
    {
        // Arrange
        var vm = CreateViewModel();

        // Act
        vm.Title = "Valid value";
        vm.Details = details;

        // Assert
        await Assert
            .That(vm.HasErrors)
            .IsEqualTo(expectHasErrors);
    }

    [Test]
    [Arguments("", false)]
    [Arguments("Valid Title", true)]
    public async Task CanSave_ReturnsExpectedResult(string title, bool expected)
    {
        // Arrange
        var vm = CreateViewModel();

        // Act
        vm.Title = title;

        // Assert
        await Assert
            .That(vm.CanSave())
            .IsEqualTo(expected);
    }

    [Test]
    public async Task UpdateDeadline_SetsDeadline_WhenHasDeadlineIsTrue()
    {
        // Arrange
        var vm = CreateViewModel();
        var date = new DateTime(2025, 1, 1);
        var time = new TimeSpan(10, 30, 0);

        // Act
        vm.HasDeadline = true;
        vm.SelectedDate = date;
        vm.SelectedTime = time;

        // Assert
        await Assert
            .That(vm.Deadline)
            .IsEqualTo(date + time);
    }

    [Test]
    public async Task ClearDeadlineCommand_SetsDeadlineToNull()
    {
        // Arrange
        var vm = CreateViewModel();
        vm.HasDeadline = true;
        vm.SelectedDate = DateTime.Today;
        vm.SelectedTime = TimeSpan.FromHours(1);

        // Act
        await Assert
            .That(vm.Deadline)
            .IsNotNull();

        vm.ClearDeadlineCommand.Execute(null);

        // Assert
        await Assert
            .That(vm.Deadline)
            .IsNull();
    }

    [Test]
    public async Task InitializeAsync_LoadsCategoriesAndSetsCategoriesList()
    {
        // Arrange
        var categories = new List<Category>
        {
            new()
            {
                Id = "1",
                Name = "Work"
            }
        };

        _todoItemsService
            .GetCategoriesAsync(Arg.Any<CancellationToken>())
            .Returns(categories);

        var vm = CreateViewModel();

        // Act
        await vm.InitializeCommand.ExecuteAsync(CancellationToken.None);

        // Assert
        await Assert
            .That(vm.Categories)
            .Contains(x => x.Name == "Work");

        await Assert
            .That(vm.Categories)
            .Contains(NoneCategory.Instance);
    }

    private TodoItemFormViewModel CreateViewModel()
    {
        _localization
            .GetString(Arg.Any<LocalizedStringKey>())
            .Returns("None");

        return new TodoItemFormViewModel(_todoItemsService, _localization);
    }
}