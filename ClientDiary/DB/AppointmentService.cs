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
    [Table]
    public class AppointmentService
    {

        #region Columns
        [Column(DbType = "INT NOT NULL Identity", IsDbGenerated = true, IsPrimaryKey = true)]
        public int AppointmentServiceId
        {
            get;
            private set;
        }

        [Column]
        private int _appointmentId;

        [Column]
        private int _serviceId;
        #endregion

        #region Associations
        
        private EntityRef<Appointment> _appointment;

        [Association(Storage = "_appointment", IsForeignKey = true, ThisKey = "_appointmentId")]
        public Appointment appointment
        {
            get
            {
                return _appointment.Entity;
            }
            set
            {
                _appointment.Entity = value;
                _appointmentId = value.AppointmentID;
            }
        }

        private EntityRef<Service> _service;

        [Association(Storage = "_service", IsForeignKey = true, ThisKey = "_serviceId")]
        public Service service
        {
            get
            {
                return _service.Entity;
            }
            set
            {
                _service.Entity = value;
                _serviceId = value.ServiceId;
            }
        }

        [Association(OtherKey = "ServiceId")]
        public EntitySet<Service> Services
        {
            get;
            private set;
        }

        [Association(OtherKey = "AppointmentID")]
        public EntitySet<Appointment> Appointments
        {
            get;
            private set;
        }

        #endregion
    }
}
