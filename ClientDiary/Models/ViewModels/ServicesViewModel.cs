using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.Models.ViewModels
{
	class ServicesViewModel : BaseViewModel
	{
		public ObservableCollection<Service> Services { get; private set; }
		DBManager _dbManager;
		public ServicesViewModel()
		{
			_dbManager = App.DBManager;
			Services = new ObservableCollection<Service>();
		}

		public override void LoadData()
		{
			base.LoadData();
			_dbManager.Services.ToList().ForEach(x => { Services.Add(x); }); ;
			this.IsDataLoaded = true;
		}

		public void AddService(Service service)
		{
			Services.Add(service);
			_dbManager.Services.InsertOnSubmit(service);
			_dbManager.SubmitChanges();
		}

		public void DeleteService(Service service)
		{
			Services.Remove(service);
			_dbManager.Services.DeleteOnSubmit(service);
			var relatedAppointmentServices = _dbManager.AppointmentServices.Where(ap => ap.ServiceId == service.ServiceId);
			_dbManager.AppointmentServices.DeleteAllOnSubmit(relatedAppointmentServices);
			_dbManager.SubmitChanges();
		}
	}
}
