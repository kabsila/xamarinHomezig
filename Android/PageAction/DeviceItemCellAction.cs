using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;

[assembly: Dependency (typeof (DeviceItemCellAction))]

namespace HomeZig.Android
{
	public class DeviceItemCellAction : IDeviceItemCell
	{
		public DeviceItemCellAction ()
		{
		}

		public void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Switch is now {0}", e.Value);
			
		}
	}
}

