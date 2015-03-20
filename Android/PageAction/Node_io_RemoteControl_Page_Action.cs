using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_RemoteControl_Page_Action))]

namespace HomeZig.Android
{
	public class Node_io_RemoteControl_Page_Action : ContentPage, I_Node_io_RemoteControl
	{
		public Node_io_RemoteControl_Page_Action ()
		{
		}

		public async void NodeIoRemoteControl_Tapped(object sender, ItemTappedEventArgs e)
		{
			var irCommand = (RemoteData)e.Item;
			foreach (var data in await App.Database.Get_flag_Login())
			{
				irCommand.remote_username = data.username;
				irCommand.node_command = "ir_remote_command";
				string jsonirCommand = JsonConvert.SerializeObject(irCommand, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine (jsonirCommand);
				WebsocketManager.websocketMaster.Send (jsonirCommand.ToString());
				break;
			}
			((ListView)sender).SelectedItem = null;

		}
	}
}

