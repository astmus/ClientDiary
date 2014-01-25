using ClientDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary
{
	public class DBManager : DataContext
	{
		public Table<Service> Services;
		public Table<Client> Clients;
		public DBManager(string connectionString = "Data source=isostore:/diary.sdf")
			: base(connectionString)
		{
			if (this.DatabaseExists() == false)
				this.CreateDatabase();
		}
	}
}
