using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class testMenuTapPage : TabbedPage
	{
		public testMenuTapPage ()
		{
			//TabbedPage mainTabbedPage = new TabbedPage ();
			//mainTabbedPage.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			///mainTabbedPage.Children.Add (new NavigationPage(new Profile_Page()){Title = "Profile"});
			//mainTabbedPage.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			//mainTabbedPage.Children.Add (new NavigationPage(new Option_Page(ipm)){Title = "Option"});
			//MainPage = mainTabbedPage;

			this.Title = "TabbedPage";
			this.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			this.Children.Add (new NavigationPage(new Profile_Page()){Title = "Profile"});
			this.Children.Add (new NavigationPage(new Powered()){Title = "test3"});

		}
	}
}

