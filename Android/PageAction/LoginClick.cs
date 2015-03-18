using System;
using WebSocket4Net;
using Newtonsoft.Json;
using Android.Util;
using Xamarin.Forms;
using System.Text;
using System.Security.Cryptography;

namespace HomeZig.Android
{
	public class LoginClick : LoginPage
	{
		public static IPageManager ipm1;
		public LoginClick (IPageManager ipm2)
		{
			ipm1 = ipm2;
			WebsocketManager websocketObject =  new WebsocketManager( websocketUrl.Text, ipm1);
			loginButton.Clicked += LoginButtonClick;
			logoutButton.Clicked += LogoutButtonClick;
			ConnectButton.Clicked += ConnectButton_Click;

		}

		public async void LoginButtonClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(LoginPage.username.Text) || String.IsNullOrEmpty(LoginPage.password.Text))
			{
				await DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
			} else {

				//code here for check valid username and password from server
				LoginPage.loginButton.IsEnabled = false;
				LoginPage.activityIndicator.IsRunning = true;
				Login loginData = new Login ();
				loginData.lastConnectWebscoketUrl = LoginPage.websocketUrl.Text;
				loginData.username = LoginPage.username.Text;
				loginData.password = sha256_hash(LoginPage.password.Text);
				loginData.flagForLogin = "";
				loginData.node_command = "check_login";

				//await App.Database.Save_Login_Item (loginData.username, LoginPage.password.Text, "pass", loginData.lastConnectWebscoketUrl);

				string jsonCommandLogin = JsonConvert.SerializeObject(loginData, Formatting.Indented);
				Log.Info ("jsonCommandLogin" , jsonCommandLogin);
				WebsocketManager.websocketMaster.Send (jsonCommandLogin);
				//ipm1.showMenuTabPage ();
			}
		}

		public async void LogoutButtonClick(object sender, EventArgs e)
		{
			await App.Database.Delete_Login_Item ();
			username.IsEnabled = true;
			password.IsEnabled = true;
			//LoginPage.loginButton.IsEnabled = true;
		}

		public async void ConnectButton_Click(object sender, EventArgs e)
		{
			loginButton.IsEnabled = false;
			ConnectButton.IsEnabled = false;
			activityIndicator.IsRunning = true;
			WebsocketManager.websocketMaster.Open ();			
		}

		public static String sha256_hash(String value) {
			StringBuilder Sb = new StringBuilder();

			using (SHA256 hash = SHA256Managed.Create()) {
				Encoding enc = Encoding.UTF8;
				Byte[] result = hash.ComputeHash(enc.GetBytes(value));

				foreach (Byte b in result)
					Sb.Append(b.ToString("x2"));
			}

			return Sb.ToString();
		}

	}
}

