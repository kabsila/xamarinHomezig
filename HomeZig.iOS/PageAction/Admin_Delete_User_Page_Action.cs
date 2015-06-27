using System;
using Xamarin.Forms;
using HomeZig.iOS;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;
using Toasts.Forms.Plugin.Abstractions;

[assembly: Dependency (typeof (Admin_Delete_User_Page_Action))]

namespace HomeZig.iOS
{
	public class Admin_Delete_User_Page_Action : ContentPage, I_Admin_Delete_User
	{
		public Admin_Delete_User_Page_Action ()
		{
		}

		public void queryUser (object sender, EventArgs e)
		{
			/**Login loginData = new Login ();
			loginData.lastConnectWebscoketUrl = LoginPage.websocketUrl.Text;
			loginData.username = "";
			loginData.password = "";
			loginData.flagForLogin = "";
			loginData.node_command = "query_user";

			string jsonCommandqueryUser = JsonConvert.SerializeObject (loginData, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("jsonCommandqueryUser {0}", jsonCommandqueryUser);
			WebsocketManager.websocketMaster.Send (jsonCommandqueryUser);**/
			//var notificator = DependencyService.Get<IToastNotificator>();
			//bool tapped = await notificator.Notify(ToastNotificationType.Error, 
			//	"Error", "Something went wrong", TimeSpan.FromSeconds(2));

		}

		public async void userForDelete_Tapped(object sender, ItemTappedEventArgs e)
		{
			var LoginItem = (LoginUsernameForDel)e.Item;

			var answer = await DisplayAlert ("Confirm?", "Would you like to delete " + LoginItem.username, "Yes", "No");
			//System.Diagnostics.Debug.WriteLine("Answer: " + answer);
			if (answer.Equals (true)) {
				LoginItem.node_command = "delete_user";
				string jsonCommandqueryUser = JsonConvert.SerializeObject (LoginItem, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine ("userForDelete_Tapped {0}", jsonCommandqueryUser);
				WebsocketManager.websocketMaster.Send (jsonCommandqueryUser);
			} else {
				((ListView)sender).SelectedItem = null;
			}

		}
	}
}

