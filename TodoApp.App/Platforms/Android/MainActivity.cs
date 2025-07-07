using Android.App;
using Android.Content.PM;
using Android.Runtime;

namespace TodoApp.App;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize
        | ConfigChanges.Orientation
        | ConfigChanges.UiMode
        | ConfigChanges.ScreenLayout
        | ConfigChanges.SmallestScreenSize
        | ConfigChanges.Density)]
[Register("com.companyname.todoapp.app.MainActivity")]
public class MainActivity : MauiAppCompatActivity { }