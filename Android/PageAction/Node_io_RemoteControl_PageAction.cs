using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_RemoteControl_PageAction))]

namespace HomeZig.Android
{
	public class Node_io_RemoteControl_PageAction : IRemoteControlCall
	{
		public Node_io_RemoteControl_PageAction ()
		{
		}

		public void tellServerForAddControl(object sender, EventArgs e)
		{
			Node_io_RemoteControl_Page.item.node_command = "add_button_remote";
			var DataForSend = JsonConvert.SerializeObject(Node_io_RemoteControl_Page.item, Formatting.Indented);
			WebsocketManager.websocketMaster.Send (DataForSend);
		}
	}
}

