using OpenQA.Selenium.Appium;

namespace TodoApp.UITests.Android;

public sealed class AppiumElementEqualityComparer : IEqualityComparer<AppiumElement>
{
    public static AppiumElementEqualityComparer Instance { get; } = new();

    public bool Equals(AppiumElement? x, AppiumElement? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x is null)
            return false;

        if (y is null)
            return false;

        if (x.GetType() != y.GetType())
            return false;

        return x.Id == y.Id;
    }

    public int GetHashCode(AppiumElement obj)
    {
        return obj.Id != null
            ? obj.Id.GetHashCode()
            : 0;
    }
}