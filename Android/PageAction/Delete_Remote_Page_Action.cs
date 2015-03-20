using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Delete_Remote_Page_Action))]

namespace HomeZig.Android
{
	public class Delete_Remote_Page_Action : ContentPage, I_Delete_Remote
	{
		public Delete_Remote_Page_Action ()
		{
		}

		public async void deleteRemote_Tapped(object sender, ItemTappedEventArgs e)
		{
			foreach (var data in await App.Database.Get_flag_Login())
			{
				var remoteItem = (RemoteData)e.Item;
				remoteItem.remote_username = data.username;
				remoteItem.node_command = "delete_button_remote";
				string jsonCommandLogin = JsonConvert.SerializeObject(remoteItem, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine (jsonCommandLogin);
				WebsocketManager.websocketMaster.Send (jsonCommandLogin.ToString());
				break;
			}

		}
	}
}

