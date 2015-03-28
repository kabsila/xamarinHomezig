using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;


[assembly: Dependency (typeof (Change_Password_Page_Action))]

namespace HomeZig.Android
{
	public class Change_Password_Page_Action : ContentPage, I_Change_Password
	{
		public Change_Password_Page_Action ()
		{
		}

		public async void changePasswordButton_Click (object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty (Change_Password_Page.newPassword.Text)) {
				await DisplayAlert ("Validation Error", "New Password are required", "Re-try");
			} else {

				string jsonCommandChangePassword = "";
				foreach (var data in await App.Database.Get_flag_Login())  
				{
					data.password = Login_Page_Action.sha256_hash (Change_Password_Page.newPassword.Text);
					data.node_command = "change_user_password";
					jsonCommandChangePassword = JsonConvert.SerializeObject (data, Formatting.Indented);
					break;
				}
				System.Diagnostics.Debug.WriteLine ("jsonCommandChangePassword {0}", jsonCommandChangePassword);
				WebsocketManager.websocketMaster.Send (jsonCommandChangePassword);
			}
		}
	}
}

