using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.Models
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class Client : BaseModel
    {
        private string _Name;

        private string _Phone;

        private int _ClientId;

        private EntitySet<Appointment> _Appointments;

        public Client()
        {
            this._Appointments = new EntitySet<Appointment>(new Action<Appointment>(this.addAppointments), new Action<Appointment>(this.deleteAppointments));
        }

        public Client(string name, string phone)
        {
            this._Appointments = new EntitySet<Appointment>(new Action<Appointment>(this.addAppointments), new Action<Appointment>(this.deleteAppointments));
            Name = name;
            Phone = phone;
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

        [Column(Storage = "_Phone", CanBeNull = false)]
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    NotifyPropertyChanging();
                    this._Phone = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Column(Storage = "_ClientId", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, IsDbGenerated = true)]
        public int ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                if ((this._ClientId != value))
                {
                    NotifyPropertyChanging();
                    this._ClientId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Client_Appointment", Storage = "_Appointments", ThisKey = "ClientId", OtherKey = "ClientId")]
        public EntitySet<Appointment> Appointments
        {
            get
            {
                return this._Appointments;
            }
            set
            {
                this._Appointments.Assign(value);
            }
        }

        private void addAppointments(Appointment entity)
        {
            NotifyPropertyChanging();
            entity.Client = this;
        }

        private void deleteAppointments(Appointment entity)
        {
            NotifyPropertyChanging();
            entity.Client = null;
        }
    }

}
