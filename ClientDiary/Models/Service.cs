using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// store data about one concrete service
namespace ClientDiary.Models
{
	[Table]
	public class Service : BaseModel
	{
		public Service()
		{

		}

		public Service(string name, double price)
		{
			_name = name;
			_price = price;
		}

		[Column(DbType = "INT NOT NULL Identity", IsDbGenerated = true, IsPrimaryKey = true)]
		public int ServiceId { get; private set; }

		// service's name
		private string _name;

		[Column]
		public string Name
		{
			get
			{
				return _name;
			}
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

		//service's price
		private double _price;

		[Column]
		public double Price
		{
			get
			{
				return _price;
			}
			set
			{
				if (_price != value)
				{
					NotifyPropertyChanging();
					_price = value;
					NotifyPropertyChanged();
				}
			}
		}

		
	}
}
