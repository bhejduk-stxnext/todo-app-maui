using TUnit.Assertions.AssertConditions.Interfaces;
using TUnit.Assertions.AssertionBuilders;

namespace TodoApp.ArchTests.Extensions;

public static class AssertionsExtensions
{
    /// <summary>
    ///     Extensions in place of HasMembers(0) assertion, because if that fails it does not show what items are inside.
    /// </summary>
    public static InvokableValueAssertionBuilder<IEnumerable<TItem>> IsNullOrEmptyCollection<TItem>(this IValueSource<IEnumerable<TItem>> valueSource)
    {
        return valueSource
            .IsNull()
            .Or.IsEquivalentTo(Array.Empty<TItem>());
    }
}