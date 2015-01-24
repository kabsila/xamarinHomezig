using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class App
	{
		static DeviceItemDatabase database;

		public static INavigation Navigation 
		{
			get;
			set;
		}

		public static Page GetMainPage ()
		{	
			return new NavigationPage(new HomePage());
		}

		public static DeviceItemDatabase Database 
		{
			get 
			{ 
				if (database == null) {
					database = new DeviceItemDatabase ();
				}
				return database; 
			}
		}
	}
}

