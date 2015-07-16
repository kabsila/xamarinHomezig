using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeZig
{
	public class App : Application, IDisposable, IPageManager
	{
		static DeviceItemDatabase database;
		static IPageManager AppIpm;
		static LoginPage lg = new LoginPage();
		public static App current;
		public static ContentPage popup;

		public App(Page mPage)
		{	
			
			MainPage = mPage;
		}

		public App()
		{	
			popup = new ContentPage();
			AppIpm = this;
			current = this;
			MainPage = lg;//lg;//new LoginPage ();		
		}
		public static INavigation Navigation 
		{
			get;
			set;
		}

		public void Dispose()
		{ 
			//Dispose(true);
			GC.SuppressFinalize(this);           
		}

		public void showMenuTabPage ()
		{	
			MainPage = new MenuTabPage (AppIpm);
		}

		public void showLoginPage ()
		{		
			MainPage = lg;
		}
		/**public static async void Check_flag_Login()
		{
			string flag = "";

			foreach (var data in await App.Database.Get_flag_Login()) {
				flag = data.flagForLogin;
				if (flag.Equals ("pass")) {
					MessagingCenter.Send<ContentPage> (new ContentPage (), "FlagLoginPass");
				} else {
					MessagingCenter.Send<ContentPage> (new ContentPage (), "FlagLoginNotPass");
				}
				break;
			}
			if (flag.Equals ("")) {
				MessagingCenter.Send<ContentPage> (new ContentPage (), "FlagLoginNotPass");
			}
		}**/


		/**public static Page GetMainPage ()
		{	
			//return new NavigationPage(new HomePage());
			//ListMainPage = ilm;
			return new HomePage ();
		}

		public static Page GetListMainPage (IPageManager ipm)
		{	
			return new MenuTabPage (ipm);
		}

		public static Page GetLoginPage ()
		{	
			return new LoginPage ();		
		}**/

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

