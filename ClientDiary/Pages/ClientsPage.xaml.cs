using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Diagnostics;
using ClientDiary.Resources;
using Microsoft.Phone;
using Microsoft.Phone.Reactive;
using System.Windows.Input;
using ClientDiary.Extensions;
using Microsoft.Phone.UserData;
using ClientDiary.Models.ViewModels;
using ClientDiary.Models;

namespace ClientDiary.Pages
{
	public partial class ClientsPage : PhoneApplicationPage
	{
		CustomMessageBox _addClientBox;
		CustomMessageBox _offerAddClientBox;
		PhoneTextBox _newClientName;
		PhoneTextBox _newClientPhone;
		ClientsViewModel _viewModel;

		public ClientsPage()
		{
			InitializeComponent();
			_viewModel = new ClientsViewModel();
			DataContext = _viewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			if (NavigationContext.QueryString.ContainsKey("action"))
			{
				string action = NavigationContext.QueryString["action"];
				switch (action)
				{
					case AppPages.Actions.Add:
						NavigationContext.QueryString.Remove("action");
						AddNewClientIconButton_Click(null, null);
						break;
					case AppPages.Actions.Edit:
						break;
				}
			}
			if (!_viewModel.IsDataLoaded)
				_viewModel.LoadData();
			//AppPages.Actions
		}

		#region properties

		CustomMessageBox addNewClientMeesageBox
		{
			get
			{
				if (_addClientBox != null)
				{
					(_addClientBox.Parent as Panel).Children.Remove(_addClientBox);
					return _addClientBox;
				}
				else
				{
					Utils.InitTextBox(ref _newClientName, "Name");
					Utils.InitTextBox(ref _newClientPhone, "Phone", InputScopeNameValue.TelephoneNumber);

					StackPanel _content = new StackPanel();
					_content.Children.Add(_newClientName);
					_content.Children.Add(_newClientPhone);

					_addClientBox = new CustomMessageBox()
					{
						Title = "Add new client",
						Message = "Enter new client info and press Ok",
						LeftButtonContent = "Ok",
						RightButtonContent = "Cancel",
						Content = _content
					};
					_addClientBox.Dismissed += NewClientInfoEntered;
					return _addClientBox;
				}
			}
		}
		#endregion

		#region Private methods

		void AddNewClient(string name, string phoneNumber)
		{
			Client client = new Client(name, phoneNumber);
			_viewModel.AddClient(client);
		}

		void DisplayMessageaboutAddNewClient(string name, string phoneNumber)
		{
			addNewClientMeesageBox.Show();
			_newClientName.Text = name;
			_newClientPhone.Text = phoneNumber;
		}

		#endregion

		#region Event handlers

		private void AddNewClientIconButton_Click(object sender, EventArgs e)
		{
			if (_offerAddClientBox != null)
				(_offerAddClientBox.Parent as Panel).Children.Remove(_offerAddClientBox);
			else
			{
				_offerAddClientBox = new CustomMessageBox()
				{
					LeftButtonContent = "From contacts",
					RightButtonContent = "By hand",
					Message = "How to add a new client?"
				};
				_offerAddClientBox.Dismissed += OnDismissed;
			}
			_offerAddClientBox.Show();
		}

		private void OnDismissed(object sender, DismissedEventArgs e)
		{

			switch (e.Result)
			{
				case CustomMessageBoxResult.LeftButton:
					PhoneNumberChooserTask task = new PhoneNumberChooserTask();
					task.Completed += ClientInfoSelectCompleted;
					task.Show();
					break;
				case CustomMessageBoxResult.RightButton:
					// little spike for correct display message box
					Scheduler.Dispatcher.Schedule(() => { DisplayMessageaboutAddNewClient("", ""); }, TimeSpan.FromMilliseconds(250));
					break;
			}
		}

		private void NewClientInfoEntered(object sender, DismissedEventArgs e)
		{
			if (e.Result == CustomMessageBoxResult.LeftButton)
				AddNewClient(_newClientName.Text, _newClientPhone.Text);
		}

		private void ClientInfoSelectCompleted(object sender, PhoneNumberResult e)
		{
			Debug.WriteLine("Selected phone number = " + e.DisplayName + " - " + e.PhoneNumber);
			Scheduler.Dispatcher.Schedule( ()=>
			{
				DisplayMessageaboutAddNewClient(e.DisplayName, e.PhoneNumber);
			},TimeSpan.FromMilliseconds(500));
		}

		private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void DeleteClient_Click(object sender, RoutedEventArgs e)
		{
			Client client = (sender as MenuItem).DataContext as Client;
			_viewModel.DeleteClient(client);
		}
		#endregion

	}
}