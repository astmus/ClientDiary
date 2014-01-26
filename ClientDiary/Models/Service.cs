using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// store data about one concrete service
namespace ClientDiary.ViewModels
{
	[Table]
	public class Service : BaseModel
	{
		[Column(DbType = "INT NOT NULL Identity", IsDbGenerated = true, IsPrimaryKey = true)]
		public int ServiceId { get; private set; }

		// service's name
		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title != value)
				{
					NotifyPropertyChanging();
					_title = value;
					NotifyPropertyChanged();
				}
			}
		}

		//service's price
		private double _price;
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
