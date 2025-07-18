﻿namespace TodoApp.UITests.Shared;

// public sealed class AppiumServer : IDisposable
// {
//     private readonly AppiumLocalService _appiumLocalService;
//
//     private const string DefaultHostAddress = "127.0.0.1";
//     private const int DefaultHostPort = 4723;
//
//     public AppiumServer(string host = DefaultHostAddress, int port = DefaultHostPort)
//     {
//         _appiumLocalService = new AppiumServiceBuilder()
//             .WithIPAddress(host)
//             .UsingPort(port)
//             .Build();
//     }
//     
//     public void Start()
//     {
//         _appiumLocalService.Start();
//     }
//
//     public void Dispose()
//     {
//         _appiumLocalService?.Dispose();
//     }
// }
//
// public abstract class AppiumSetup : IDisposable
// {
//
//     public AppiumDriver App { get; }
//
//     public AppiumSetup()
//     {
//         // If you started an Appium server manually, make sure to comment out the next line
//         // This line starts a local Appium server for you as part of the test run
//         AppiumServerHelper.StartAppiumLocalServer();
//
//         var androidOptions = new AppiumOptions
//         {
//             // Specify UIAutomator2 as the driver, typically don't need to change this
//             AutomationName = "UIAutomator2",
//             // Always Android for Android
//             PlatformName = "Android",
//             // This is the Android version, not API level
//             // This is ignored if you use the avd option below
//             PlatformVersion = "13",
//             // The full path to the .apk file to test or the package name if the app is already installed on the device
//             App = "com.companyname.basicappiumsample",
//         };
//
//         // Specifying the avd option will boot the emulator for you
//         // make sure there is an emulator with the name below
//         // If not specified, make sure you have an emulator booted
//         //androidOptions.AddAdditionalAppiumOption("avd", "pixel_5_-_api_33");
//
//         // Note there are many more options that you can use to influence the app under test according to your needs
//
//         App = new AndroidDriver(androidOptions);
//     }
//
//     public void Dispose()
//     {
//         driver?.Quit();
//
//         // If an Appium server was started locally above, make sure we clean it up here
//         AppiumServerHelper.DisposeAppiumLocalServer();
//     }
// }