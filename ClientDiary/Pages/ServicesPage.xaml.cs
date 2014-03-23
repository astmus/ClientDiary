using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using ClientDiary.Models;
using ClientDiary.Models.ViewModels;
using ClientDiary.Resources;

namespace ClientDiary.Pages
{
	public partial class ServicesPage : PhoneApplicationPage
	{
		CustomMessageBox _addNewServiceBox;
		PhoneTextBox _serviceName;
		PhoneTextBox _servicePrice;
		ServicesViewModel _viewModel;

		public ServicesPage()
		{
			InitializeComponent();
			BuildLocalizedApplicationBar();
			_viewModel = new ServicesViewModel();
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
						AddServiceIconButton_Click(null, null);
						break;
					case AppPages.Actions.Edit:
						break;
				}
			}
			if (!_viewModel.IsDataLoaded)
				_viewModel.LoadData();
		}

		#region Private methods

		void DisplayAddNewServiceOffer()
		{
			if (_addNewServiceBox != null)
				(_addNewServiceBox.Parent as Panel).Children.Remove(_addNewServiceBox);
			else
			{
				Utils.InitTextBox(ref _serviceName, AppResources.UIServicesName);
				Utils.InitTextBox(ref _servicePrice, AppResources.UIServicesPrice, InputScopeNameValue.Number);
				StackPanel _content = new StackPanel();
				_content.Children.Add(_serviceName);
				_content.Children.Add(_servicePrice);
				_addNewServiceBox = new CustomMessageBox()
				{
					Title = AppResources.UIMessageAddNewService,
					Message = AppResources.UIMessageEnterServiceInfo,
					RightButtonContent = AppResources.UICancel,
					LeftButtonContent = "Ok",
					Content = _content
				};
				_addNewServiceBox.Dismissed += OnNewServiceInfoEntered;
			}
			_addNewServiceBox.Show();
			_serviceName.Text = String.Empty;
			_servicePrice.Text = String.Empty;			
		}

		void OnNewServiceInfoEntered(object sender, DismissedEventArgs e)
		{
			if (e.Result != CustomMessageBoxResult.LeftButton) return;
			if (_serviceName.Text == "") return;
			if (_servicePrice.Text == "") return;

			float price = float.Parse(_servicePrice.Text);
			Service s = new Service(_serviceName.Text, price);
			_viewModel.AddService(s);
		}

		#endregion

		#region Event handlers

		private void AddServiceIconButton_Click(object sender, EventArgs e)
		{
			DisplayAddNewServiceOffer();
		}
		
		private void DeleteClient_Click(object sender, RoutedEventArgs e)
		{
			Service s = (sender as MenuItem).DataContext as Service;
			_viewModel.DeleteService(s);
		}

		#endregion

		private void BuildLocalizedApplicationBar()
		{
			// Set the page's ApplicationBar to a new instance of ApplicationBar.
			ApplicationBar = new ApplicationBar();

			// Create a new button and set the text value to the localized string from AppResources.
			ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/Icons/add.png", UriKind.Relative));
			appBarButton.Text = AppResources.UIAddNewService;
			appBarButton.Click += AddServiceIconButton_Click;
			ApplicationBar.Buttons.Add(appBarButton);
		}
	}
}