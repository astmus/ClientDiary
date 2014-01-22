using System;
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

		public static Uri Clients
		{
			get 
			{
				return _clients ?? (_clients = new Uri("/Pages/Clients.xaml",UriKind.Relative)); 
			}
		}

		public static Uri Services
		{
			get
			{
				return _services ?? (_services = new Uri("/Pages/Services.xaml", UriKind.Relative));
			}
		}

		public static Uri Statistic
		{
			get
			{
				return _statistic ?? (_statistic = new Uri("/Pages/Statistic.xaml", UriKind.Relative));
			}
		}

		public static Uri Settings
		{
			get
			{
				return _settings ?? (_settings = new Uri("/Pages/Settings.xaml", UriKind.Relative));
			}
		}
	}
}
