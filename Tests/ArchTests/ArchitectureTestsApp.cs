using TodoApp.App;
using TodoApp.App.Pages;
using TodoApp.ArchTests.Extensions;

namespace TodoApp.ArchTests;

public class ArchitectureTestsApp
{
    [Test]
    public async Task App_ShouldNot_ReferenceCoreExceptDI()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(AppAssembly.Assembly)
            .That()
            // exclude DI
            .DoNotHaveName(nameof(MauiProgram))
            .ShouldNot()
            .HaveDependencyOn("TodoApp.Core.Services")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task Pages_Should_BeInPagesNamespaceAndEndWithPage()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(AppAssembly.Assembly)
            .That()
            .HaveNameEndingWith("Page")
            .Should()
            .ResideInNamespace("TodoApp.App.Pages")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task Views_Should_BeInViewsNamespaceAndEndWithPage()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(AppAssembly.Assembly)
            .That()
            .HaveNameEndingWith("View")
            .Should()
            .ResideInNamespace("TodoApp.App.Views")
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }

    [Test]
    public async Task Pages_Should_InheritFromBasePage()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(AppAssembly.Assembly)
            .That()
            .HaveNameEndingWith("Page")
            .Should()
            .Inherit(typeof(BaseContentPage<>))
            .GetResult();

        // Assert
        await Assert
            .That(result.FailingTypes)
            .IsNullOrEmptyCollection();
    }
}