using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.ViewModels
{
	class ClientsViewModel : BaseViewModel
	{
		public ObservableCollection<Client> Clients { get; private set; }
		DBManager _dbManager;
		public ClientsViewModel()
		{
			_dbManager = App.DbManager;
			Clients = new ObservableCollection<Client>();
		}

		public override void LoadData()
		{
			base.LoadData();
			_dbManager.Clients.ToList().ForEach(x => { Clients.Add(x); }); ;
			this.IsDataLoaded = true;
		}
	}
}
