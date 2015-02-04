using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_ItemPageAction))]

namespace HomeZig.Android
{
	public class Node_io_ItemPageAction : IDeviceCall
	{
		public Node_io_ItemPageAction ()
		{

		}

		public void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			string jsonCommandIo = JsonConvert.SerializeObject(DeviceItemCell.NodeItem, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json", jsonCommandIo);
			//WebsocketManager.websocketMaster.Send (jsonCommandIo);
		}

		public void switchLeft_OnChange(object sender, ToggledEventArgs e)
		{
			Node_io_ItemPage.item.node_command = "io_command";
			var aStringBuilder = new StringBuilder(NumberConversion.hex2binary (Node_io_ItemPage.item.node_io));
			aStringBuilder.Remove(6, 1);
			aStringBuilder.Insert(6, Convert.ToByte(e.Value).ToString());
			Node_io_ItemPage.item.node_io = NumberConversion.binary2hex(aStringBuilder.ToString());

			string jsonCommandIo = JsonConvert.SerializeObject(Node_io_ItemPage.item, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json222", jsonCommandIo);
			//WebsocketManager.websocketMaster.Send (jsonCommandIo);
		}

		public void switchRight_OnChange(object sender, ToggledEventArgs e)
		{
			Node_io_ItemPage.item.node_command = "io_command";
			var aStringBuilder = new StringBuilder(NumberConversion.hex2binary (Node_io_ItemPage.item.node_io));
			aStringBuilder.Remove(7, 1);
			aStringBuilder.Insert(7, Convert.ToByte(e.Value).ToString());
			Node_io_ItemPage.item.node_io = NumberConversion.binary2hex(aStringBuilder.ToString());

			string jsonCommandIo = JsonConvert.SerializeObject(Node_io_ItemPage.item, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json222", jsonCommandIo);
			//WebsocketManager.websocketMaster.Send (jsonCommandIo);
		}

	}
}

