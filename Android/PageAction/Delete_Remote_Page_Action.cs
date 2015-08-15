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

		/**public async void deleteRemote_Tapped(object sender, ItemTappedEventArgs e)
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

		}**/

		public async void deleteRemote_Tapped(RemoteData rd)
		{
			var answer = await DisplayAlert ("Confirm?", "Would you like to delete " + rd.remote_button_name, "Yes", "No");
			//System.Diagnostics.Debug.WriteLine("Answer: " + answer);
			if (answer.Equals (true)) {
				rd.remote_button_name = rd.remote_button_name;
				rd.node_command = "delete_button_remote";
				string jsonCommandaddRemoteButton = JsonConvert.SerializeObject(rd, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine ("{0}",jsonCommandaddRemoteButton);
				WebsocketManager.websocketMaster.Send (jsonCommandaddRemoteButton.ToString());
			} else {
				
			}

		}
	}
}

