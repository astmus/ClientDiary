using ClientDiary.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClientDiary.Models
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class Appointment : BaseModel
    {
        private System.DateTime _DueDate;

        private int _AppointmentId = -1;

        private System.Data.Linq.Link<int> _ClientId;

        private EntitySet<AppointmentService> _AppointmentServices;

        private EntityRef<Client> _Client;

		#region constructors
		public Appointment()
        {
            this._AppointmentServices = new EntitySet<AppointmentService>(new Action<AppointmentService>(this.AddAppointmentService), new Action<AppointmentService>(this.DeleteAppointmentService));
            this._Client = default(EntityRef<Client>);
        }

		public Appointment(Client client, List<Service> services, DateTime dueDate)
		{
			this._AppointmentServices = new EntitySet<AppointmentService>(new Action<AppointmentService>(this.AddAppointmentService), new Action<AppointmentService>(this.DeleteAppointmentService));
			Client = client;
			AddServices(services);
			DueDate = dueDate;
		}
		#endregion

		#region properties

		public List<Service> Services
		{
			get
			{
				if (AppointmentId == -1) return null;
				List<Service> res = App.DBManager.Services.Where(s => App.DBManager.AppointmentServices.Where(a => a.AppointmentId == AppointmentId).Select(ap => ap.ServiceId).Contains(s.ServiceId)).ToList();
				return res;
			}
		}

		public String ServicesList
		{
			get 
			{
				return string.Join(", ", Services.Select(s => s.Name).ToArray());
			}
		}

		public SolidColorBrush HighlightedColor
		{
			get{
				TimeSpan d = _DueDate - DateTime.Now;
				return Application.Current.Resources[d < Settings.Instance.HighLightedAppointmentsTime ? "PhoneAccentBrush" : "PhoneForegroundBrush"] as SolidColorBrush;
			}
			
		}

		#endregion

		#region public methods

		public void AddServices(List<Service> services)
		{
			foreach (Service serv in services)
			{
				AppointmentService s = new AppointmentService();
				s.Service = serv;
				AddAppointmentService(s);
			}
		}

		#endregion

		#region table data

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DueDate")]
        public System.DateTime DueDate
        {
            get
            {
                return this._DueDate;
            }
            set
            {
                if ((this._DueDate != value))
                {
                    NotifyPropertyChanging();
                    this._DueDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AppointmentId", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, IsDbGenerated = true)]
        public int AppointmentId
        {
            get
            {
                return this._AppointmentId;
            }
            set
            {
                if ((this._AppointmentId != value))
                {
                    NotifyPropertyChanging();
                    this._AppointmentId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClientId")]
        private int ClientId
        {
            get
            {
                return this._ClientId.Value;
            }
            set
            {
                if ((this._ClientId.Value != value))
                {
                    if (this._Client.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    NotifyPropertyChanging();
                    this._ClientId.Value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Appointment_AppointmentService", Storage = "_AppointmentServices", ThisKey = "AppointmentId", OtherKey = "AppointmentId")]
        public EntitySet<AppointmentService> AppointmentServices
        {
            get
            {
                return this._AppointmentServices;
            }
            set
            {
                this._AppointmentServices.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Client_Appointment", Storage = "_Client", ThisKey = "ClientId", OtherKey = "ClientId", IsForeignKey = true)]
        public Client Client
        {
            get
            {
                return this._Client.Entity;
            }
            set
            {
                Client previousValue = this._Client.Entity;
                if (((previousValue != value)
                            || (this._Client.HasLoadedOrAssignedValue == false)))
                {
                    NotifyPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Client.Entity = null;
                        previousValue.Appointments.Remove(this);
                    }
                    this._Client.Entity = value;
                    if ((value != null))
                    {
                        value.Appointments.Add(this);
                        this._ClientId.Value = value.ClientId;
                    }
                    else
                    {
                        this._ClientId.Value = default(int);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

		#endregion

		#region private methods
		private void AddAppointmentService(AppointmentService entity)
        {
            NotifyPropertyChanging();
            entity.Appointment = this;
        }

        private void DeleteAppointmentService(AppointmentService entity)
        {
            NotifyPropertyChanging(); 
            entity.Appointment = null;
		}
		#endregion
	}
}