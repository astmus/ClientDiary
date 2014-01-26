using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public bool IsDataLoaded
		{
			get;
			protected set;
		}

		/// <summary>
		/// Load data for model
		/// </summary>
		virtual public void LoadData()
		{
			
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
