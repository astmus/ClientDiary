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
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Phone;
using Microsoft.Phone.Reactive;
using ClientDiary.Controls;
using System.Windows.Media;
using ClientDiary.Models;
using ClientDiary.Models.ViewModels;
using ClientDiary.Pages;


namespace ClientDiary
{
	public partial class MainPage : PhoneApplicationPage
	{
		DBManager _dbManager;
		private static AppointmentsViewModel _clientsRecords = null;
		
		// Constructor
		public MainPage()
		{
			InitializeComponent();
			BuildLocalizedApplicationBar();
			// Set the data context of the LongListSelector control to the sample data
			_clientsRecords = new AppointmentsViewModel();
			DataContext = _clientsRecords;
			_dbManager = App.DBManager;
			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
		}

		// Load data for the ViewModel Items
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (!_clientsRecords.IsDataLoaded)
				_clientsRecords.LoadData();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (e.Content is ClientsPage)
				(e.Content as ClientsPage).DeletingClient += OnDeleteClient;
		}

		void OnDeleteClient(Client client)
		{
			var list = _clientsRecords.Records.Where(app => app.Client.ClientId == client.ClientId).ToList();
			foreach(Appointment i in list)
				_clientsRecords.Records.Remove(i);
			/*for (int i = 0; i < _clientsRecords.Records.Count; ++i)
				if (_clientsRecords.Records[i].Client.ClientId == client.ClientId)
				{
					_clientsRecords.Records.RemoveAt(i);
					--i;
				}
			 * */
		}

		// Handle selection changed on LongListSelector
		private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If selected item is null (no selection) do nothing
			if (MainLongListSelector.SelectedItem == null)
				return;
			// Navigate to the new page
			//NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ClientRecord).ID, UriKind.Relative));

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
			CustomMessageBox message = null;
			if (_dbManager.Clients.Count() == 0)
			{
				message = new CustomMessageBox()
				{
					Message = AppResources.UIMessageArentAnyClients,
					LeftButtonContent = AppResources.UIAdd,
					RightButtonContent = AppResources.UICancel,
					Tag = AppPages.Clients
				};
			}
			else
			if (_dbManager.Services.Count() == 0)
			{
				message = new CustomMessageBox()
				{
					Message = AppResources.UIMessageArentAnyServices,
					LeftButtonContent = AppResources.UIAdd,
					RightButtonContent = AppResources.UICancel,
					Tag = AppPages.Services
				};				
			}
            else
            {
                var v = new NewAppointmentBox();
                v.Show();
                v.Dismissed += v_Dismissed;
            }

			if (message != null)
			{
				message.Dismissed += OnDismissed;
				message.Show();
				return;
			}

		}

        void v_Dismissed(object sender, NewAppointentBoxResult e)
        {
            switch (e.ActionResult)
            {
                case NewAppointmentBoxActionResult.Added:
					Appointment app = new Appointment();
					app.DueDate = DateTime.Now;
					app.Client = e.SelectedClient;
					app.AddServices(e.SelectedServices);
					_clientsRecords.AddAppointment(app);
                    break;
                case NewAppointmentBoxActionResult.Canceled:
                    break;
            }
        }

		void OnDismissed(object sender, DismissedEventArgs e)
		{
			CustomMessageBox box = sender as CustomMessageBox;
			if (e.Result == CustomMessageBoxResult.LeftButton)
			{
				Uri addClientUri = AppPages.AddAction(box.Tag as Uri,AppPages.Actions.Add);
				//little spike for correct display CustomMessageBox
				Scheduler.Dispatcher.Schedule(() => { NavigationService.Navigate(addClientUri); }, TimeSpan.FromMilliseconds(230));	
			}
		}

		// Sample code for building a localized ApplicationBar
		private void BuildLocalizedApplicationBar()
		{
			// Set the page's ApplicationBar to a new instance of ApplicationBar.
			ApplicationBar = new ApplicationBar();

			/*
			 
		 <shell:ApplicationBar.MenuItems>
                <shell:ApplicationBarMenuItem Text="Clients" Click="ClientsMenuItem_Click" />
                <shell:ApplicationBarMenuItem Text="Services" Click="ServicesMenuItem_Click"/>
                <shell:ApplicationBarMenuItem Text="Statistic" Click="StatisticMenuItem_Click"/>
            </shell:ApplicationBar.MenuItems>
            <shell:ApplicationBarIconButton IconUri="/Images/Icons/add.png" Text="add record" Click="AddServiceRecordIconButton_Click"/>
            <shell:ApplicationBarIconButton IconUri="/Images/Icons/feature.settings.png" Text="settings" Click="SettingsIconButton_Click"/>
        </shell:ApplicationBar>
			 
			 */


			// Create a new button and set the text value to the localized string from AppResources.
			ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/Icons/add.png", UriKind.Relative));
			appBarButton.Text = AppResources.UIAddRecord;
			appBarButton.Click += AddServiceRecordIconButton_Click;
			ApplicationBar.Buttons.Add(appBarButton);

			appBarButton = new ApplicationBarIconButton(new Uri("/Images/Icons/feature.settings.png", UriKind.Relative));
			appBarButton.Text = AppResources.UISettings;
			appBarButton.Click += SettingsIconButton_Click;
			ApplicationBar.Buttons.Add(appBarButton);

			// Create a new menu item with the localized string from AppResources.
			ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.UIClients);
			appBarButton.Click += ClientsMenuItem_Click;
			ApplicationBar.MenuItems.Add(appBarMenuItem);

			appBarMenuItem = new ApplicationBarMenuItem(AppResources.UIServices);
			appBarButton.Click += ServicesMenuItem_Click;
			ApplicationBar.MenuItems.Add(appBarMenuItem);

			appBarMenuItem = new ApplicationBarMenuItem(AppResources.UIStatistic);
			appBarButton.Click += StatisticMenuItem_Click;
			ApplicationBar.MenuItems.Add(appBarMenuItem);
		}
	}
}