using System;
using System.Collections.ObjectModel;
using MahApps.Metro.IconPacks;
using TP1.Mvvm;
using TP1.Views;

namespace TP1.ViewModels
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
                NavigationType = typeof(PuntoAVM),
                NavigationDestination = new Uri("Views/PuntoA.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaB },
                Label = "Punto B",
                NavigationType = typeof(PuntoB),
                NavigationDestination = new Uri("Views/PuntoB.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaC },
                Label = "Punto C",
                NavigationType = typeof(PuntoC),
                NavigationDestination = new Uri("Views/PuntoC.xaml", UriKind.RelativeOrAbsolute)
            });

            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid },
                Label = "Settings",
                NavigationType = typeof(SettingsPage),
                NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid },
                Label = "About",
                NavigationType = typeof(AboutPage),
                NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}