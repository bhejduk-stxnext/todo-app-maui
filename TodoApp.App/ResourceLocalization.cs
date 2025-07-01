using System.Globalization;
using TodoApp.ViewModels.Localization;
using Strings = TodoApp.App.Resources.Strings.Strings;

namespace TodoApp.App;

public sealed class ResourceLocalization : ILocalization
{
    public string GetString(LocalizedStringKey key)
    {
        var localizedString = Strings.ResourceManager.GetString(key.Key, CultureInfo.CurrentUICulture);
        return localizedString ?? throw new KeyNotFoundException($"Resource string with key: {key.Key} was not found.");
    }
}