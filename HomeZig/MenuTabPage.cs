using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class MenuTabPage : TabbedPage
	{

		public MenuTabPage ()
		{
			this.Children.Add (new NavigationPage(new DeviceTypeListPage()){Title = "Powered"});
			//this.Children.Add (new DeviceTypeListPage(){Title = "Powered"});
			this.Children.Add (new NavigationPage(new Outlet2()){Title = "test2"});
			this.Children.Add (new NavigationPage(new Powered()){Title = "test3"});
			//this.Children.Add (new DeviceListPage{Title = "test4"});
		}
	}
}

