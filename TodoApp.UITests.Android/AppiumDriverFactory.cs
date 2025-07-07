using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace TodoApp.UITests.Android;

public sealed class AppiumDriverFactory
{
    public static AppiumDriver Create(string appPath)
    {
        var androidOptions = new AppiumOptions
        {
            AutomationName = AutomationName.AndroidUIAutomator2,
            PlatformName = "Android",
            App = appPath
        };

        androidOptions.AddAdditionalAppiumOption(
            "uiautomator2ServerInstallTimeout",
            TimeSpan.FromSeconds(120)
                .TotalMilliseconds);

        androidOptions.AddAdditionalAppiumOption(
            "uiautomator2ServerLaunchTimeout",
            TimeSpan.FromSeconds(120)
                .TotalMilliseconds);

        var app = new AndroidDriver(androidOptions, TimeSpan.FromSeconds(180));

        app
            .Manage()
            .Timeouts()
            .ImplicitWait = TimeSpan.FromSeconds(10);

        return app;
    }
}