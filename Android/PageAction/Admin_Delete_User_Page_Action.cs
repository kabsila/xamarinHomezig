using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;


[assembly: Dependency (typeof (Admin_Delete_User_Page_Action))]

namespace HomeZig.Android
{
	public class Admin_Delete_User_Page_Action : ContentPage, I_Admin_Delete_User
	{
		public Admin_Delete_User_Page_Action ()
		{
		}

		public void queryUser (object sender, EventArgs e)
		{
			Login loginData = new Login ();
			loginData.lastConnectWebscoketUrl = LoginPage.websocketUrl.Text;
			loginData.username = "";
			loginData.password = "";
			loginData.flagForLogin = "";
			loginData.node_command = "query_user";

			string jsonCommandqueryUser = JsonConvert.SerializeObject (loginData, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("jsonCommandqueryUser {0}", jsonCommandqueryUser);
			WebsocketManager.websocketMaster.Send (jsonCommandqueryUser);
		}

		public async void userForDelete_Tapped(object sender, ItemTappedEventArgs e)
		{
			var Item = (Login)e.Item;

			Item.lastConnectWebscoketUrl = "";
			Item.password = "";
			Item.flagForLogin = "";
			Item.node_command = "delete_user";

			string jsonCommandqueryUser = JsonConvert.SerializeObject (Item, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("userForDelete_Tapped {0}", jsonCommandqueryUser);
			WebsocketManager.websocketMaster.Send (jsonCommandqueryUser);
			System.Diagnostics.Debug.Write("complteteeeeeeeeeeeeeeeeeeeeeeeeeeeee");
		}
	}
}

