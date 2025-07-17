using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;

namespace TodoApp.UITests.Android;

public static class AppiumDriverExtensions
{
    public static AppiumElement FindById(this AppiumDriver driver, string id)
    {
        return driver.FindElement(By.Id(id));
    }

    public static IReadOnlyCollection<AppiumElement> FindAllById(this AppiumDriver driver, string id)
    {
        return driver.FindElements(By.Id(id));
    }

    public static AppiumElement FindById(this AppiumElement element, string id)
    {
        return element.FindElement(By.Id(id));
    }

    public static IReadOnlyCollection<AppiumElement> FindAllById(this AppiumElement element, string id)
    {
        return element.FindElements(By.Id(id));
    }

    public static async Task WaitForUiAsync(this AppiumDriver driver, int milliseconds = 1000)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(milliseconds));
    }

    public static async Task SwipeDownAsync(this AppiumDriver driver, AppiumElement swipeView, TimeSpan? duration = null)
    {
        duration ??= TimeSpan.FromMilliseconds(500);

        var location = swipeView.Location;
        var size = swipeView.Size;
        var startX = location.X + size.Width / 2;
        var startY = location.Y + (int)(size.Height * 0.9);
        var endY = location.Y + (int)(size.Height * 0.1);

        var touchInput = new PointerInputDevice(PointerKind.Touch);
        var actionSequence = new ActionSequence(touchInput, 0);

        actionSequence.AddAction(
            touchInput.CreatePointerMove(
                CoordinateOrigin.Viewport,
                startX,
                startY,
                TimeSpan.Zero));

        actionSequence.AddAction(touchInput.CreatePointerDown(MouseButton.Touch));

        actionSequence.AddAction(
            touchInput.CreatePointerMove(
                CoordinateOrigin.Viewport,
                startX,
                endY,
                duration.Value));

        actionSequence.AddAction(touchInput.CreatePointerUp(MouseButton.Touch));

        driver.PerformActions(
            new List<ActionSequence>
            {
                actionSequence
            });

        await Task.Delay(duration.Value);
    }
}