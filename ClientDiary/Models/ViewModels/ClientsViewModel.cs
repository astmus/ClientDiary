using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.Models.ViewModels
{
	class ClientsViewModel : BaseViewModel
	{
		public ObservableCollection<Client> Clients { get; private set; }
		DBManager _dbManager;
		public ClientsViewModel()
		{
			_dbManager = App.DBManager;
			Clients = new ObservableCollection<Client>();
		}

		public override void LoadData()
		{
			base.LoadData();
            int cou = _dbManager.Clients.Count();
            _dbManager.Clients.ToList().ForEach(x => { Clients.Add(x); });
			this.IsDataLoaded = true;
		}

		public void AddClient(Client client)
		{
			Clients.Add(client);
			_dbManager.Clients.InsertOnSubmit(client);
			_dbManager.SubmitChanges();
		}

		public void DeleteClient(Client client)
		{
			Clients.Remove(client);
			_dbManager.Clients.DeleteOnSubmit(client);
			_dbManager.SubmitChanges();
		}
	}
}
