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
			_dbManager = App.DBManager;
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
					Message = "There aren't any clients",
					LeftButtonContent = "Add new client",
					RightButtonContent = "Cancel",
					Tag = AppPages.Clients
				};
			}
			else
			if (_dbManager.Services.Count() == 0)
			{
				message = new CustomMessageBox()
				{
					Message = "There aren't any services",
					LeftButtonContent = "Add new service",
					RightButtonContent = "Cancel",
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



			// add new client record
            
		}

        void v_Dismissed(object sender, NewAppointentBoxResult e)
        {
            switch (e.ActionResult)
            {
                case NewAppointmentBoxActionResult.Added:
					Appointment app = new Appointment();
					app.DueDate = DateTime.Now;
					app.Client = e.SelectedClient;
					App.DBManager.Appointments.InsertOnSubmit(app);
					App.DBManager.SubmitChanges();
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