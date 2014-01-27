using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientDiary.Resources;

namespace ClientDiary.Models.ViewModels
{
	public class ClientsRecordsViewModel : BaseViewModel
	{
		public ClientsRecordsViewModel()
		{
			this.Records = new ObservableCollection<ClientRecord>();
		}

		/// <summary>
		/// A collection for ClientsRecords objects.
		/// </summary>
		public ObservableCollection<ClientRecord> Records { get; private set; }

		/// <summary>
		/// Load data for view
		/// </summary>
		public override void LoadData()
		{
			// Sample data; replace with real data
			this.Records.Add(new ClientRecord() { ID = "0", Name = "client1", Services = "service procedure 1" });
			this.Records.Add(new ClientRecord() { ID = "1", Name = "client2", Services = "service procedure 2" });

			this.IsDataLoaded = true;
		}

		
	}
}