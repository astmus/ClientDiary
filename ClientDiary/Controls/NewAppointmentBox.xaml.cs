using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ClientDiary.Models;
using System.Collections;

namespace ClientDiary.Controls
{
    public enum NewAppointmentBoxDismissRes
    {
        Added,
        Canceled
    }

    public partial class NewAppointmentBox : UserControl
    {
        PhoneApplicationFrame _frame;
        PhoneApplicationPage _page;
        Color _systemTrayColor;
        
        #region events
        public event EventHandler<NewAppointmentBoxDismissRes> Dismissed;
        #endregion

        #region properties
		//public List<Client> Clients
		//{
		//	get;
		//	set;
		//}

		//public List<Service> Services
		//{
		//	get;
		//	set;
		//}
        #endregion

        public NewAppointmentBox()
        {
            InitializeComponent();			
			servicesPicker.SummaryForSelectedItemsDelegate = Summarize;
			clientPicker.ItemsSource = App.DBManager.Clients;
			servicesPicker.ItemsSource = App.DBManager.Services;
        }

        public void Show()
        {
            _frame = Application.Current.RootVisual as PhoneApplicationFrame;
            _page = _frame.Content as PhoneApplicationPage;
            (_page.Content as Panel).Children.Add(this);
            _systemTrayColor = SystemTray.BackgroundColor;
            SystemTray.BackgroundColor = (Color)Application.Current.Resources["PhoneChromeColor"];
            _page.BackKeyPress += PageBackKeyPress;
        }

        #region private methods

		private string Summarize(IList items)
		{
			if (items == null) return "select services";
			string [] names = items.Cast<Service>().Select(x => x.Name).ToArray();
			return string.Join(", ", names);
		}

        void RiseDissmisEvent(NewAppointmentBoxDismissRes result)
        {
            if (Dismissed != null)
                Dismissed(this, result);
        }

        void CloseBox()
        {
            (this.Parent as Panel).Children.Remove(this);
            _page.BackKeyPress -= PageBackKeyPress;
            SystemTray.BackgroundColor = _systemTrayColor;
        }

        #endregion

        void PageBackKeyPress(object sender, 
            System.ComponentModel.CancelEventArgs e)
        {
            CloseBox();
            e.Cancel = true;
            RiseDissmisEvent(NewAppointmentBoxDismissRes.Canceled);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CloseBox();
            RiseDissmisEvent(NewAppointmentBoxDismissRes.Added);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseBox();
            RiseDissmisEvent(NewAppointmentBoxDismissRes.Canceled);
        }

        
    }
}
