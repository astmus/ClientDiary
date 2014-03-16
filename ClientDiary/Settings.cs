using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDiary
{
	public class Settings
	{
		static Settings _instance;
		static Object _sync = new Object();

		private Settings()
		{
			HighLightedAppointmentsTime = TimeSpan.FromDays(7);
		}

		public static Settings Instance
		{
			get
			{
				lock (_sync)
				{
					if (_instance == null)
						_instance = new Settings();
				}
				return _instance;
			}
		}

		public TimeSpan HighLightedAppointmentsTime
		{
			get;
			set;
		}
	
	}
}
