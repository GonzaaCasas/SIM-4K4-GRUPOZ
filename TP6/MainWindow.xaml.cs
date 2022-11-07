﻿using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using MenuItem = TP6.ViewModels.MenuItem;

namespace TP6 {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow {
		private readonly Navigation.NavigationServiceEx navigationServiceEx;
		public MainWindow() {
			InitializeComponent();

			this.navigationServiceEx = new Navigation.NavigationServiceEx();
			this.navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
			this.HamburgerMenuControl.Content = this.navigationServiceEx.Frame;

			// Navigate to the home page.
			this.Loaded += (sender, args) => this.navigationServiceEx.Navigate(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute));
		}

		private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e) {
			if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation) {
				this.navigationServiceEx.Navigate(menuItem.NavigationDestination);
			}
		}

		private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e) {
			// select the menu item
			this.HamburgerMenuControl.SetCurrentValue(HamburgerMenu.SelectedItemProperty,
					this.HamburgerMenuControl.Items
							.OfType<MenuItem>()
							.FirstOrDefault(x => x.NavigationDestination == e.Uri));
			this.HamburgerMenuControl.SetCurrentValue(HamburgerMenu.SelectedOptionsItemProperty,
					this.HamburgerMenuControl
							.OptionsItems
							.OfType<MenuItem>()
							.FirstOrDefault(x => x.NavigationDestination == e.Uri));

			// or when using the NavigationType on menu item
			// this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
			//                                              .Items
			//                                              .OfType<MenuItem>()
			//                                              .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());
			// this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
			//                                                     .OptionsItems
			//                                                     .OfType<MenuItem>()
			//                                                     .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());

			// update back button
			this.GoBackButton.SetCurrentValue(VisibilityProperty, this.navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed);
		}

		private void GoBack_OnClick(object sender, RoutedEventArgs e) {
			this.navigationServiceEx.GoBack();
		}

		private void LaunchGitHubSite(object sender, RoutedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/GonzaaCasas/SIM-4K4-GRUPOZ");
		}
	}
}
