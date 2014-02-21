using ClientDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.DB
{
    // represent table for create many-to-many associations
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class AppointmentService : BaseModel
    {
        private int _AppointmentServiceId;

        private int _AppointmentId;

        private int _ServiceId;

        private EntityRef<Appointment> _Appointment;

        private EntityRef<Service> _Service;
		
        public AppointmentService()
        {
            this._Appointment = default(EntityRef<Appointment>);
            this._Service = default(EntityRef<Service>);
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AppointmentServiceId", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, IsDbGenerated = true)]
        public int AppointmentServiceId
        {
            get
            {
                return this._AppointmentServiceId;
            }
            set
            {
                if ((this._AppointmentServiceId != value))
                {
                    NotifyPropertyChanging();
                    this._AppointmentServiceId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AppointmentId")]
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
                    if (this._Appointment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    NotifyPropertyChanging();
                    this._AppointmentId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceId")]
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
                    if (this._Service.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    NotifyPropertyChanging();
                    this._ServiceId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Appointment_AppointmentService", Storage = "_Appointment", ThisKey = "AppointmentId", OtherKey = "AppointmentId", IsForeignKey = true)]
        public Appointment Appointment
        {
            get
            {
                return this._Appointment.Entity;
            }
            set
            {
                Appointment previousValue = this._Appointment.Entity;
                if (((previousValue != value)
                            || (this._Appointment.HasLoadedOrAssignedValue == false)))
                {
                    NotifyPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Appointment.Entity = null;
                        previousValue.AppointmentServices.Remove(this);
                    }
                    this._Appointment.Entity = value;
                    if ((value != null))
                    {
                        value.AppointmentServices.Add(this);
                        this._AppointmentId = value.AppointmentId;
                    }
                    else
                    {
                        this._AppointmentId = default(int);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Service_AppointmentService", Storage = "_Service", ThisKey = "ServiceId", OtherKey = "ServiceId", IsForeignKey = true)]
        public Service Service
        {
            get
            {
                return this._Service.Entity;
            }
            set
            {
                Service previousValue = this._Service.Entity;
                if (((previousValue != value)
                            || (this._Service.HasLoadedOrAssignedValue == false)))
                {
                    NotifyPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Service.Entity = null;
                        previousValue.AppointmentServices.Remove(this);
                    }
                    this._Service.Entity = value;
                    if ((value != null))
                    {
                        value.AppointmentServices.Add(this);
                        this._ServiceId = value.ServiceId;
                    }
                    else
                    {
                        this._ServiceId = default(int);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
