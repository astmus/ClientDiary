using ClientDiary.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// store data about one concrete service
namespace ClientDiary.Models
{
    [Table]
    public partial class Service : BaseModel
    {
        private string _Name;

        private float _Price;

        private int _ServiceId;

        private EntitySet<AppointmentService> _AppointmentServices;

        public Service()
        {
            this._AppointmentServices = new EntitySet<AppointmentService>(new Action<AppointmentService>(this.attach_AppointmentServices), new Action<AppointmentService>(this.detach_AppointmentServices));
        }

        public Service(string name, float price)
        {
            this._AppointmentServices = new EntitySet<AppointmentService>(new Action<AppointmentService>(this.attach_AppointmentServices), new Action<AppointmentService>(this.detach_AppointmentServices));
            Name = name;
            Price = price;
        }

		public override string ToString()
		{
			return _Name;
		}

        [Column(Storage = "_Name", CanBeNull = false)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    NotifyPropertyChanging();
                    this._Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Column(Storage = "_Price")]
        public float Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                if ((this._Price != value))
                {
                    NotifyPropertyChanging();
                    this._Price = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Column(Storage = "_ServiceId", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, IsDbGenerated = true)]
        public int ServiceId
        {
            get
            {
                return this._ServiceId;
            }
            set
            {
                if ((this._ServiceId != value))
                {
                    NotifyPropertyChanging();
                    this._ServiceId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Service_AppointmentService", Storage = "_AppointmentServices", ThisKey = "ServiceId", OtherKey = "ServiceId")]
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

        private void attach_AppointmentServices(AppointmentService entity)
        {
            NotifyPropertyChanging();
            entity.Service = this;
        }

        private void detach_AppointmentServices(AppointmentService entity)
        {
            NotifyPropertyChanged();
            entity.Service = null;
        }
    }

}
