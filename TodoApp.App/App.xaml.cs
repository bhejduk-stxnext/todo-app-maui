namespace TodoApp.App;

public partial class App : Application
{
    private readonly AppShell _appShell;

    public App(AppShell appShell)
    {
        _appShell = appShell;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_appShell);

#if WINDOWS
        // emulate phone size
        const int Newheight = 846;
        const int Newwidth = 412;

        window.Height = window.MinimumHeight = window.MaximumHeight = Newheight;
        window.Width = window.MinimumWidth = window.MaximumWidth = Newwidth;
#endif
        return window;
    }
}