using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.Models
{
	[Table]
	public class Client : BaseModel
	{
		public Client()
		{

		}

		public Client(string name, string phoneNumber)
		{
			Name = name;
			PhoneNumber = phoneNumber;
		}

		[Column(DbType = "INT NOT NULL Identity", IsDbGenerated = true, IsPrimaryKey = true)]
		public int ClientId { get; private set; }

		private string _name;

		[Column]
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					NotifyPropertyChanging();
					_name = value;
					NotifyPropertyChanged();
				}
			}
		}

		private string _phoneNumber;

		[Column]
		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set
			{
				if (_phoneNumber != value)
				{
					NotifyPropertyChanging();
					_phoneNumber = value;
					NotifyPropertyChanged();
				}
			}
		}
	}
}
