using TodoApp.ArchTests.Extensions;
using TodoApp.ViewModels;

namespace TodoApp.ArchTests;

public class ArchitectureTestsViewModels
{
    [Test]
    public async Task ViewModels_ShouldNot_DependOnApp()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ViewModelsAssembly.Assembly)
            .ShouldNot()
            .HaveDependencyOn("TodoApp.App")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task ViewModels_Should_BeInViewModelsNamespace()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ViewModelsAssembly.Assembly)
            .That()
            .HaveNameEndingWith("ViewModel")
            .Should()
            .ResideInNamespace("TodoApp.ViewModels")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task ViewModels_Should_InheritFromBaseViewModel()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ViewModelsAssembly.Assembly)
            .That()
            .HaveNameEndingWith("ViewModel")
            .And()
            // exclude BaseViewModel
            .DoNotHaveName(nameof(BaseViewModel))
            .Should()
            .Inherit(typeof(BaseViewModel))
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task ViewModels_Should_HaveViewModelsSuffix()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ViewModelsAssembly.Assembly)
            .That()
            .Inherit(typeof(BaseViewModel))
            .Should()
            .HaveNameEndingWith("ViewModel")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }
}