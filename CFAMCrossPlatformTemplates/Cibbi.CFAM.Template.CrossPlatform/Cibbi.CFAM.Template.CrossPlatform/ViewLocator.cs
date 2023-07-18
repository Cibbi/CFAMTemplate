using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Cibbi.CFAM.Template.CrossPlatform.ViewModels;
using Cibbi.CFAM.ViewModels.Windows;
using Cibbi.CFAM.Views.Windows;
using ReactiveUI;

namespace Cibbi.CFAM.Template.CrossPlatform;

public class ViewLocator : IViewLocator
{
#if (WindowType == 'SimpleWindow')
    public static MainSimpleWindowViewModel MainViewModel { get; } = new() {WindowName = "Cibbi.CFAM" , Content = new MainContentViewModel() };
    public static MainSimpleWindow MainWindow { get; } = new(){DataContext = ViewModels.MainViewModel};
#endif
        
#if (WindowType == 'FluentWindow')
    public static MainFluentWindowViewModel MainViewModel { get; } = new() {WindowName = "Cibbi.CFAM"};
    public static MainFluentWindow MainWindow { get; } = new(){DataContext = ViewModels.MainViewModel};
#endif
        
    public static ViewLocator Current { get; } = new();
        
    public IViewFor ResolveView<T>(T viewModel, string? contract = null)
    {
        string? name = viewModel?.GetType().FullName!.Replace("ViewModel", "View");
        if(name is null) throw new ArgumentOutOfRangeException(nameof(viewModel));
        var type = Type.GetType(name);
        if (type is null) throw new ArgumentOutOfRangeException(nameof(viewModel));
        return (IViewFor)Activator.CreateInstance(type)!;
    }
}