﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClientDiary.ViewModels
{
	public class CustomerRecord : INotifyPropertyChanged
	{
		private string _id;
		/// <summary>
		/// this property is used to identify the object.
		/// </summary>
		/// <returns></returns>
		public string ID
		{
			get
			{
				return _id;
			}
			set
			{
				if (value != _id)
				{
					_id = value;
					NotifyPropertyChanged("ID");
				}
			}
		}

		private string _name;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value != _name)
				{
					_name = value;
					NotifyPropertyChanged("Name");
				}
			}
		}

		private string _services;
		/// <summary>
		/// this property is used in the view to display list of selected services
		/// </summary>
		/// <returns></returns>
		public string Services
		{
			get
			{
				return _services;
			}
			set
			{
				if (value != _services)
				{
					_services = value;
					NotifyPropertyChanged("Services");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}