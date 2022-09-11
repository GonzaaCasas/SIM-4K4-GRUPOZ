using MahApps.Metro.IconPacks;
using System;
using System.Collections.ObjectModel;
using TP4.Mvvm;
using TP4.Views;

namespace TP4.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> OptionsMenu { get; } = new ObservableCollection<MenuItem>();


        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.HouseDoor },
                Label = "Inicio",
                NavigationType = typeof(MainPage),
                NavigationDestination = new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaA },
                Label = "Punto A",
                NavigationType = typeof(PuntoA),
                NavigationDestination = new Uri("Views/PuntoA.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaB },
                Label = "Punto B",
                NavigationType = typeof(PuntoB),
                NavigationDestination = new Uri("Views/PuntoB.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}