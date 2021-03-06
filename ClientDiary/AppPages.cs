﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary
{
	public static class AppPages
	{
		static Uri _clients;
		static Uri _services;
		static Uri _statistic;
		static Uri _settings;

		public static class Actions
		{
			public const string Add = "add";
			public const string Edit = "edit";
		}

		public static Uri AddAction(Uri page, string action)
		{
			return new Uri(String.Format("{0}?action={1}",page.OriginalString,action),UriKind.Relative);
		}

		public static Uri Clients
		{
			get 
			{
				return _clients ?? (_clients = new Uri("/Pages/ClientsPage.xaml",UriKind.Relative)); 
			}
		}

		public static Uri Services
		{
			get
			{
				return _services ?? (_services = new Uri("/Pages/ServicesPage.xaml", UriKind.Relative));
			}
		}

		public static Uri Statistic
		{
			get
			{
				return _statistic ?? (_statistic = new Uri("/Pages/StatisticPage.xaml", UriKind.Relative));
			}
		}

		public static Uri Settings
		{
			get
			{
				return _settings ?? (_settings = new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
			}
		}
	}
}
