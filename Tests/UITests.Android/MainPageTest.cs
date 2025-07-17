using OpenQA.Selenium.Appium;

namespace TodoApp.UITests.Android;

[NotInParallel]
public class MainPageTest
{
    [ClassDataSource<AppTestContext>(Shared = SharedType.PerClass)]
    public required AppTestContext AppTestContext { get; set; }

    [Before(Test)]
    public async Task ResetApp()
    {
        AppTestContext.ResetApp();
        await AppTestContext.Driver.WaitForUiAsync(3000);
    }

    [Test]
    public async Task ShouldHave20ItemsLoaded()
    {
        // Arrange
        var app = AppTestContext.Driver;

        var todoItemsScrollView = app.FindById("TodoItemsScroll");
        var allItems = new HashSet<AppiumElement>(AppiumElementEqualityComparer.Instance);

        // Act
        while (true)
        {
            // Check if new items appeared
            var visibleItems = app.FindAllById("TodoItem");

            var itemsAdded = visibleItems
                .Select(allItems.Add)
                .ToList();

            var anyItemAdded = itemsAdded.Any(x => x);

            if (!anyItemAdded)
                break;

            await app.SwipeDownAsync(todoItemsScrollView);
        }

        // Assert
        await Assert
            .That(allItems)
            .HasCount(20);
    }

    [Test]
    public async Task ClickOnTodoItemRedirectsToEditPage()
    {
        // Arrange
        var app = AppTestContext.Driver;
        var firstTodoItem = app.FindById("TodoItem");

        // Act
        firstTodoItem.Click();

        await app.WaitForUiAsync();

        var redirectedPage = app.FindById("EditTodoItemPage");

        // Assert
        await Assert
            .That(redirectedPage)
            .IsNotNull();
    }

    [Test]
    public async Task EditItemTitleChangedTitleOnMainPage()
    {
        // Arrange
        var app = AppTestContext.Driver;

        var todoItem = app.FindById("TodoItem");

        var todoItemTitle = todoItem.FindById("TodoItemTitle");

        await Assert
            .That(todoItemTitle.Text)
            .IsNotEmpty();

        // Act
        todoItem.Click();

        await app.WaitForUiAsync();

        var titleEntry = app.FindById("TitleEntry");
        titleEntry.SendKeys("Updated Title");

        var saveButton = app.FindById("SaveButton");
        saveButton.Click();

        await app.WaitForUiAsync();

        // Assert
        var updatedTodoItem = app.FindById("TodoItem");

        var updatedItemTitle = updatedTodoItem.FindById("TodoItemTitle");

        await Assert
            .That(updatedItemTitle.Text)
            .IsEqualTo("Updated Title");
    }
}