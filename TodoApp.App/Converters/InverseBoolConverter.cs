using System.Globalization;

namespace TodoApp.App.Converters;

/// <summary>
///     Reverses bool value.
/// </summary>
public sealed class InverseBoolConverter : IValueConverter
{
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value is not bool boolean
            ? throw new ArgumentException("Value must be bool", nameof(value))
            : !boolean;
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value is not bool boolean
            ? throw new ArgumentException("Value must be bool", nameof(value))
            : !boolean;
    }
}