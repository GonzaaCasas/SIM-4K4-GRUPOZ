using MahApps.Metro.IconPacks;
using System;
using System.Collections.ObjectModel;
using TP5.Mvvm;
using TP5.Views;

namespace TP5.ViewModels {
	public class ShellViewModel : BindableBase {
		public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();

		public ObservableCollection<MenuItem> OptionsMenu { get; } = new ObservableCollection<MenuItem>();


		public ShellViewModel() {
			// Build the menus
			this.Menu.Add(new MenuItem() {
				Icon = new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.HouseDoor },
				Label = "Inicio",
				NavigationType = typeof(MainPage),
				NavigationDestination = new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute)
			});
			this.Menu.Add(new MenuItem() {
				Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaI },
				Label = "Ingreso Datos",
				NavigationType = typeof(PuntoA),
				NavigationDestination = new Uri("Views/PuntoA.xaml", UriKind.RelativeOrAbsolute)
			});
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaG },
                Label = "Grilla",
                NavigationType = typeof(HojaGrilla),
                NavigationDestination = new Uri("Views/HojaGrilla.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem() {
				Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AlphaR },
				Label = "Resultados",
				NavigationType = typeof(PuntoB),
				NavigationDestination = new Uri("Views/PuntoB.xaml", UriKind.RelativeOrAbsolute)
			});
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Help },
                Label = "Resultados",
                NavigationType = typeof(PuntoB),
                NavigationDestination = new Uri("Views/Ayuda.xaml", UriKind.RelativeOrAbsolute)
            });

        }
	}
}