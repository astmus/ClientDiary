using ClientDiary.DB;
using ClientDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary
{
    public partial class DBManager : DataContext
    {
        public DBManager(string connection = "Data source=isostore:/diary.sdf") :
            base(connection)
        {
            if (this.DatabaseExists() == false)
                this.CreateDatabase();
        }

        public System.Data.Linq.Table<Client> Clients
        {
            get
            {
                return this.GetTable<Client>();
            }
        }

        public System.Data.Linq.Table<Service> Services
        {
            get
            {
                return this.GetTable<Service>();
            }
        }

        public System.Data.Linq.Table<Appointment> Appointments
        {
            get
            {
                return this.GetTable<Appointment>();
            }
        }

        public System.Data.Linq.Table<AppointmentService> AppointmentServices
        {
            get
            {
                return this.GetTable<AppointmentService>();
            }
        }
    }
}
