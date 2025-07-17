using TodoApp.App;
using TodoApp.ArchTests.Extensions;
using TodoApp.Core;
using TodoApp.ViewModels;

namespace TodoApp.ArchTests;

public class ArchitectureTestsCommon
{
    [Test]
    public async Task Classes_Should_BeAbstractOrSealed()
    {
        // Arrange

        // Act
        var result = Types
            .InAssemblies(
            [
                AppAssembly.Assembly,
                ViewModelsAssembly.Assembly,
                CoreAssembly.Assembly
            ])
            .That()
            .AreClasses()
            .And()
            .DoNotResideInNamespace("TodoApp.App.Resources")
            .And()
            .DoNotResideInNamespaceContaining("XamlGeneratedCode")
            .Should()
            .BeAbstract()
            .Or()
            .BeSealed()
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task Interfaces_Should_StartWithI()
    {
        // Arrange

        // Act
        var result = Types
            .InAssemblies(
            [
                AppAssembly.Assembly,
                ViewModelsAssembly.Assembly,
                CoreAssembly.Assembly
            ])
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }
}