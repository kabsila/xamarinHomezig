using System;
using Xamarin.Forms;


namespace HomeZig
{
	public class MenuTabPage : Application
	{

		public MenuTabPage (IPageManager ipm)
		{
			TabbedPage mainTabbedPage = new TabbedPage ();
			mainTabbedPage.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			mainTabbedPage.Children.Add (new NavigationPage(new Outlet2()){Title = "test2"});
			mainTabbedPage.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			mainTabbedPage.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});

			MainPage = mainTabbedPage;
			/**
			this.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			//this.Children.Add (new DeviceTypeListPage(){Title = "Powered"});
			this.Children.Add (new NavigationPage(new Outlet2()){Title = "test2"});
			this.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			//if (Check_admin ()) {
			this.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});
			**/
				//this.Children.Add (new NavigationPage(new Admin_Add_User_Page()){Title = "Add User"});
			//}


		}

		/**async bool Check_admin()
		{
			bool is_admin = false;
			foreach (var data in await App.Database.Get_flag_Login()) 
			{
				if(data.username.Equals("admin")){
					is_admin = true;
				}
				break;
			}

			return is_admin;
		}**/
	}
}

