using OpenQA.Selenium.Appium;

namespace TodoApp.UITests.Android;

public sealed class AppTestContext : IDisposable
{
    public AppiumDriver Driver { get; }

    public AppTestContext()
    {
        var appPath = Environment.GetEnvironmentVariable("APP_PATH");
        ArgumentNullException.ThrowIfNull(appPath, "Missing -e APP_PATH environment variable");

        Driver = AppiumDriverFactory.Create(appPath);
    }

    public void ResetApp()
    {
        Driver.TerminateApp("com.companyname.todoapp.app");
        Driver.ActivateApp("com.companyname.todoapp.app");
    }

    public void Dispose()
    {
        Driver.Quit();
    }
}