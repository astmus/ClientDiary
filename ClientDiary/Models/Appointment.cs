using ClientDiary.DB;
using System;
using System.ComponentModel;
using System.Data.Linq;
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
    [Table]
	public class Appointment : BaseModel
    {
        #region Columns

        [Column(DbType = "INT NOT NULL Identity", IsDbGenerated = true, IsPrimaryKey = true)]
        public int AppointmentID
        {
            get;
            private set;
        }

        [Column]
        public DateTime EventTime
        {
            get; set;
        }

        #endregion

        #region Associations

        [Association(OtherKey = "AppointmentServiceId")]
        public EntitySet<AppointmentService> AppointmentServices
        {
            get;
            private set;
        }

        [Column]
        private int _clientId;
        private EntityRef<Client> _client;

        [Association(Storage = "_client", IsForeignKey = true, ThisKey = "_clientId")]
        public Client client
        {
            get
            {
                return _client.Entity;
            }
            set
            {
                _client.Entity = value;
                _clientId = value.ClientId;
            }
        }

        #endregion

    }
}