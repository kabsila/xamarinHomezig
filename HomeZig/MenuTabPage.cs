using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class MenuTabPage : TabbedPage
	{

		//IPageManager ipm2;

		public MenuTabPage (IPageManager ipm)
		{
			//ipm2 = ipm;
			//TabbedPage mainTabbedPage = new TabbedPage ();
			//mainTabbedPage.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			//mainTabbedPage.Children.Add (new NavigationPage(new Profile_Page()){Title = "Profile"});
			//mainTabbedPage.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			//mainTabbedPage.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});

			//MainPage = mainTabbedPage;


			this.Title = "TabbedPage";
			this.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			this.Children.Add (new NavigationPage(new Profile_Page()){Title = "Profile"});
			this.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			this.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});
			/**this.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			//this.Children.Add (new DeviceTypeListPage(){Title = "Powered"});
			this.Children.Add (new NavigationPage(new Outlet2()){Title = "test2"});
			this.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			//if (Check_admin ()) {
			this.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});**/

				//this.Children.Add (new NavigationPage(new Admin_Add_User_Page()){Title = "Add User"});
			//}


		}

	/**	protected override void OnStart()
		{
			System.Diagnostics.Debug.WriteLine ("OnStart");
		}
		protected override void OnSleep()
		{
			base.OnSleep ();
			System.Diagnostics.Debug.WriteLine ("OnSleep");
		}
		protected override void OnResume()
		{
			System.Diagnostics.Debug.WriteLine ("OnResume");
		}**/

	}
}

