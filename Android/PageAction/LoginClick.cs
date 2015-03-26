using System;
using WebSocket4Net;
using Newtonsoft.Json;
using Android.Util;
using Xamarin.Forms;
using System.Text;
using System.Security.Cryptography;
using System.Timers;
using Toasts.Forms.Plugin.Abstractions;

namespace HomeZig.Android
{
	public class LoginClick : LoginPage
	{
		public static IPageManager ipm1;
		public static Timer tmr ;
		public LoginClick (IPageManager ipm2)
		{
			ipm1 = ipm2;
			//WebsocketManager websocketObject =  new WebsocketManager(ipm1);
			loginButton.Clicked += LoginButtonClick;
			logoutButton.Clicked += LogoutButtonClick;
			ConnectButton.Clicked += ConnectButton_Click;

		}

		public async void LoginButtonClick(object sender, EventArgs e)
		{
			LoginPage.loginFail.IsVisible = false;
			if (String.IsNullOrEmpty(LoginPage.username.Text) || String.IsNullOrEmpty(LoginPage.password.Text))
			{
				await DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
			} else {

				//code here for check valid username and password from server
				Device.BeginInvokeOnMainThread (() => {
					LoginPage.loginButton.IsEnabled = false;
					LoginPage.activityIndicator.IsRunning = true;
				});
				Login loginData = new Login ();
				loginData.lastConnectWebscoketUrl = LoginPage.websocketUrl.Text;
				loginData.username = LoginPage.username.Text;
				loginData.password = sha256_hash(LoginPage.password.Text);
				loginData.flagForLogin = "";
				loginData.node_command = "check_login";

				//await App.Database.Save_Login_Item (loginData.username, LoginPage.password.Text, "pass", loginData.lastConnectWebscoketUrl);

				string jsonCommandLogin = JsonConvert.SerializeObject(loginData, Formatting.Indented);
				Log.Info ("jsonCommandLogin" , jsonCommandLogin);
				//WebsocketManager.websocketMaster.Send (jsonCommandLogin);
				WebsocketManager.websocketMaster.Send("{\"cmd_login\":[{\"username\":\"admin\",\"flagForLogin\":\"pass\",\"lastConnectWebscoketUrl\":\"ws://echo.websocket.org\"}]}");
				//ipm1.showMenuTabPage ();
			}
		}

		public async void LogoutButtonClick(object sender, EventArgs e)
		{
			await App.Database.Delete_Login_Item ();
			await App.Database.Delete_RemoteData_Item();
			await App.Database.Delete_All_Login_Username_Show_For_Del ();

			Device.BeginInvokeOnMainThread (() => {
				LoginPage.username.Text = "";
				LoginPage.password.Text = "";
				username.IsEnabled = true;
				password.IsEnabled = true;
				LoginPage.loginButton.IsEnabled = true;
				LoginPage.logoutButton.IsEnabled = false;
				LoginPage.activityIndicator.IsRunning = false;
			});

			//LoginPage.loginButton.IsEnabled = true;
		}

		public async void ConnectButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(LoginPage.websocketUrl.Text))
			{
				await DisplayAlert("Validation Error", "Server URL is required", "Re-try");
			} else {
				tmr = new Timer();
				tmr.Interval = 4000; // 0.1 second
				tmr.Elapsed += timerHandler; // We'll write it in a bit
				tmr.Start();

				new WebsocketManager ();
				loginButton.IsEnabled = false;
				ConnectButton.IsEnabled = false;
				activityIndicator.IsRunning = true;
				try{
					WebsocketManager.websocketMaster.Open ();
				}catch{
					tmr.Stop ();
					LoginPage.activityIndicator.IsRunning = false;
					LoginPage.ConnectButton.IsEnabled = true;
				}

			}
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

		private void timerHandler(object sender, EventArgs e) {
			WebsocketManager.websocketMaster.Dispose ();
			Device.BeginInvokeOnMainThread (async () => {
				var notificator = DependencyService.Get<IToastNotificator>();
				await notificator.Notify(ToastNotificationType.Warning, 
					"TimeOut", "Check your internet connection", TimeSpan.FromSeconds(3));
				LoginPage.ConnectButton.IsEnabled = true;
				LoginPage.activityIndicator.IsRunning = false;
			});


			tmr.Stop(); // Manually stop timer, or let run indefinitely
		}

	}
}

