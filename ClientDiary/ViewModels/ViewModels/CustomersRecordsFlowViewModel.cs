using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientDiary.Resources;

namespace ClientDiary.ViewModels
{
	public class CustomersRecordsFlowViewModel : INotifyPropertyChanged
	{
		public CustomersRecordsFlowViewModel()
		{
			this.Items = new ObservableCollection<CustomerRecord>();
		}

		/// <summary>
		/// A collection for CustomerRecords objects.
		/// </summary>
		public ObservableCollection<CustomerRecord> Items { get; private set; }

		public bool IsDataLoaded
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates and adds a few ItemViewModel objects into the Items collection.
		/// </summary>
		public void LoadData()
		{
			// Sample data; replace with real data
			this.Items.Add(new CustomerRecord() { ID = "0", Name = "client1", Services = "service procedure 1"});
			this.Items.Add(new CustomerRecord() { ID = "1", Name = "client2", Services = "service procedure 2"});
			
			this.IsDataLoaded = true;
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