using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class App
	{
		static DeviceItemDatabase database;
		static IPageManager ListMainPage;

		public static INavigation Navigation 
		{
			get;
			set;
		}

		public static Page GetMainPage ()
		{	
			//return new NavigationPage(new HomePage());
			//ListMainPage = ilm;
			return new HomePage ();
		}

		public static Page GetListMainPage ()
		{	
			return new MenuTabPage ();
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

