using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cibbi.CFAM.Extensions;
using Cibbi.CFAM.Services;
using Cibbi.CFAM.Template.CrossPlatform.ViewModels;
using Cibbi.CFAM.Template.CrossPlatform.Views;
using ReactiveUI;
using Splat;

namespace Cibbi.CFAM.Template.CrossPlatform;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        Locator.CurrentMutable
            .RegisterAnd<DialogProvider>()
            .RegisterSingletonAnd<IPagesProvider, PagesProvider>()
            .RegisterSingletonAnd<IViewLocator, ViewLocator>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = ViewLocator.MainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = ViewLocator.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}