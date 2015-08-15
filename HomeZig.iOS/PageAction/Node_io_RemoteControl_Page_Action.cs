using System;
using Xamarin.Forms;
using HomeZig.iOS;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_RemoteControl_Page_Action))]

namespace HomeZig.iOS
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

		//public void submitRenameButton_Click(object sender, EventArgs e)
		public void submitRenameButton_Click(RemoteData remote, string newName)
		{
			//System.Diagnostics.Debug.WriteLine ("submitRenameButton_Click");
			//var b = (Rename_Remote_Page)sender;
			//var itemRemote = (RemoteData)b.BindingContext;
			RemoteData itemRemote =  new RemoteData();
			itemRemote.remote_button_name = remote.remote_button_name;//Rename_Remote_Page.item.remote_button_name;
			itemRemote.node_command = "ir_remote_rename";
			itemRemote.new_button_name = newName;//Rename_Remote_Page.RemoteName_value.Text;
			itemRemote.node_addr = remote.node_addr;
			itemRemote.remote_username = remote.remote_username;
			string jsonCommandaddRemoteButton = JsonConvert.SerializeObject(itemRemote, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("{0}",jsonCommandaddRemoteButton);
			WebsocketManager.websocketMaster.Send (jsonCommandaddRemoteButton.ToString());
		}
		public void cancelRenameButton_Click(object sender, EventArgs e)
		{

		}
	}
}

