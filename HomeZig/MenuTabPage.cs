using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class MenuTabPage : TabbedPage
	{

		public MenuTabPage ()
		{
			this.Children.Add (new DeviceListPage(){Title = "test1"});
			this.Children.Add (new Outlet2(){Title = "test2"});
			this.Children.Add (new Powered{Title = "test3"});
			this.Children.Add (new DeviceListPage{Title = "test4"});
		}
	}
}

