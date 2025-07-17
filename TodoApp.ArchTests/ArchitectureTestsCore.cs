using TodoApp.ArchTests.Extensions;
using TodoApp.Core;

namespace TodoApp.ArchTests;

public class ArchitectureTestsCore
{
    [Test]
    public async Task Core_ShouldNot_DependOnAppOrViewModels()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(CoreAssembly.Assembly)
            .ShouldNot()
            .HaveDependencyOn("TodoApp.App")
            .And()
            .HaveDependencyOn("TodoApp.ViewModels")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }
}