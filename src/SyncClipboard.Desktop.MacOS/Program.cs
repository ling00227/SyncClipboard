﻿using AppKit;
using Avalonia;
using SyncClipboard.Core.Commons;
using SyncClipboard.Core.Models.UserConfigs;
using SyncClipboard.Core.Utilities;
using System;

namespace SyncClipboard.Desktop.MacOS;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        if (AppInstance.EnsureSingleInstance(args) is false)
        {
            return;
        }
        NSApplication.Init();

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            App.Current?.Logger?.Write($"UnhandledException {e.GetType()} {e.Message} \n {e.StackTrace}");
            App.Current?.AppCore?.Stop();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        var hide = new ConfigBase(Env.UserConfigFile).GetConfig<ProgramConfig>().HideWindowOnStartup;
        return AppBuilder.Configure(() => new App(AppServices.ConfigureServices()))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .With(new MacOSPlatformOptions { ShowInDock = !hide });
    }
}
