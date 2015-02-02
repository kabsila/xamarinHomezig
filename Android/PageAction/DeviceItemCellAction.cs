using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;


[assembly: Dependency (typeof (DeviceItemCellAction))]

namespace HomeZig.Android
{
	public class DeviceItemCellAction : IDeviceItemCell
	{
		public DeviceItemCellAction ()
		{

		}


		public async void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			string jsonCommandIo = JsonConvert.SerializeObject(DeviceItemCell.NodeItem, Formatting.None);
			System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json", jsonCommandIo);
			//WebsocketManager.websocketMaster.Send (jsonCommandIo);
		}

	}
}

