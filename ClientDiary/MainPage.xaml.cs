using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClientDiary.Resources;
using ClientDiary.ViewModels;

namespace ClientDiary
{
	public partial class MainPage : PhoneApplicationPage
	{
		DBManager _dbManager;
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			// Set the data context of the LongListSelector control to the sample data
			DataContext = App.WorkFlowDataContext;
			_dbManager = App.DbManager;
			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
		}

		// Load data for the ViewModel Items
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (!App.WorkFlowDataContext.IsDataLoaded)
			{
				App.WorkFlowDataContext.LoadData();
			}
		}

		// Handle selection changed on LongListSelector
		private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If selected item is null (no selection) do nothing
			if (MainLongListSelector.SelectedItem == null)
				return;
			// Navigate to the new page
			NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ClientRecord).ID, UriKind.Relative));

			// Reset selected item to null (no selection)
			MainLongListSelector.SelectedItem = null;
		}

		private void ClientsMenuItem_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(AppPages.Clients);
		}

		private void ServicesMenuItem_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(AppPages.Services);
		}

		private void StatisticMenuItem_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(AppPages.Statistic);
		}

		private void SettingsIconButton_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(AppPages.Settings);
		}

		private void AddServiceRecordIconButton_Click(object sender, EventArgs e)
		{
			CustomMessageBox message;
			if (_dbManager.Clients.Count() != 0)
			{
				message = new CustomMessageBox()
				{
					Content = "There aren't any clients",
					LeftButtonContent = "Add new client",
					RightButtonContent = "Cancel",
					Tag = AppPages.Clients
				};
				message.Dismissed += OnDismissed;
				message.Show();
				return;
			}
			
			if (_dbManager.Services.Count() == 0)
			{
				message = new CustomMessageBox()
				{
					Content = "There aren't any services",
					LeftButtonContent = "Add new service",
					RightButtonContent = "Cancel",
					Tag = AppPages.Services
				};
				message.Dismissed += OnDismissed;
				message.Show();
				return;
			}


		}

		void OnDismissed(object sender, DismissedEventArgs e)
		{
			CustomMessageBox box = sender as CustomMessageBox;
			if (e.Result == CustomMessageBoxResult.LeftButton)
			{
				Uri addClientUri = AppPages.AddAction(box.Tag as Uri,AppPages.Actions.Add);
				NavigationService.Navigate(addClientUri);	
			}
		}

		// Sample code for building a localized ApplicationBar
		//private void BuildLocalizedApplicationBar()
		//{
		//    // Set the page's ApplicationBar to a new instance of ApplicationBar.
		//    ApplicationBar = new ApplicationBar();

		//    // Create a new button and set the text value to the localized string from AppResources.
		//    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
		//    appBarButton.Text = AppResources.AppBarButtonText;
		//    ApplicationBar.Buttons.Add(appBarButton);

		//    // Create a new menu item with the localized string from AppResources.
		//    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
		//    ApplicationBar.MenuItems.Add(appBarMenuItem);
		//}
	}
}