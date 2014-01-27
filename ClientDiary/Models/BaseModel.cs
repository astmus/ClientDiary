using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.Models
{
	public class BaseModel : INotifyPropertyChanged, INotifyPropertyChanging
	{
		// interface realization
		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var propertyChangedCopy = PropertyChanged;
			if (propertyChangedCopy != null)
			{
				propertyChangedCopy(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
		{
			var propertyChangingCopy = PropertyChanging;
			if (propertyChangingCopy != null)
			{
				propertyChangingCopy(this, new PropertyChangingEventArgs(propertyName));
			}
		}
	}
}
