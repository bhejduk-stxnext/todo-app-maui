namespace TodoApp.App;

public partial class App : Application
{
    private readonly AppShell _appShell;

    public App(IServiceProvider serviceProvider)
    {
        // https://github.com/dotnet/maui/issues/11485 <- Static resources (color) cannot are not loaded before DI
        InitializeComponent();
        _appShell = serviceProvider.GetRequiredService<AppShell>();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_appShell);

#if WINDOWS
        // emulate phone size
        const int NewHeight = 846;
        const int NewWidth = 412;

        window.Height = window.MinimumHeight = window.MaximumHeight = NewHeight;
        window.Width = window.MinimumWidth = window.MaximumWidth = NewWidth;
#endif
        return window;
    }
}