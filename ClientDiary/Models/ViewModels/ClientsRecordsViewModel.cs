using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientDiary.Resources;
using System.Linq;

namespace ClientDiary.Models.ViewModels
{
	public class AppointmentsViewModel : BaseViewModel
	{
		public AppointmentsViewModel()
		{
			this.Records = new ObservableCollection<Appointment>();			
		}

		/// <summary>
		/// A collection for ClientsRecords objects.
		/// </summary>
		public ObservableCollection<Appointment> Records { get; private set; }

		/// <summary>
		/// Load data for view
		/// </summary>
		public override void LoadData()
		{			
			// Sample data; replace with real data
			//this.Records.Add(new Appointment() { ID = "0", Name = "client1", Services = "service procedure 1" });
			//this.Records.Add(new Appointment() { ID = "1", Name = "client2", Services = "service procedure 2" });
			App.DBManager.Appointments.OrderBy(x => x.DueDate).ToList().ForEach(x => { Records.Add(x); });
			this.IsDataLoaded = true;
		}

		public void AddAppointment(Appointment appointment)
		{		
			int i = 0;
			while (Records[i].DueDate < appointment.DueDate)
				i++;

			Records.Insert(i, appointment);
			App.DBManager.Appointments.InsertOnSubmit(appointment);
			App.DBManager.SubmitChanges();
		}

		public void DeleteAppointment(Appointment appointment)
		{
			Records.Remove(appointment);
			App.DBManager.Appointments.DeleteOnSubmit(appointment);
			App.DBManager.SubmitChanges();
		}
	}
}