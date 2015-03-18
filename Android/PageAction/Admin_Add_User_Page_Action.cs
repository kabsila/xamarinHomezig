using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;


[assembly: Dependency (typeof (Admin_Add_User_Page_Action))]

namespace HomeZig.Android
{
	public class Admin_Add_User_Page_Action : ContentPage, I_Admin_Add_User
	{
		public Admin_Add_User_Page_Action ()
		{
		}

		public async void registerButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty (Admin_Add_User_Page.username.Text) || String.IsNullOrEmpty (Admin_Add_User_Page.password.Text)) {
				DisplayAlert ("Validation Error", "Username and Password are required", "Re-try");
			} else {
				Login loginData = new Login ();
				loginData.lastConnectWebscoketUrl = LoginPage.websocketUrl.Text;
				loginData.username = Admin_Add_User_Page.username.Text;
				loginData.password = LoginClick.sha256_hash (Admin_Add_User_Page.password.Text);
				loginData.flagForLogin = "";
				loginData.node_command = "add_user";

				//await App.Database.Save_Login_Item (loginData.username, LoginPage.password.Text, "pass", loginData.lastConnectWebscoketUrl);

				string jsonCommandLogin = JsonConvert.SerializeObject (loginData, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine ("jsonCommandLogin {0}", jsonCommandLogin);
				WebsocketManager.websocketMaster.Send (jsonCommandLogin);
			}
		}
	}
}

