using System;
using Xamarin.Forms;


namespace HomeZig
{
	public class LoginPage : ContentPage
	{
		public static Entry websocketUrl = new Entry
		{
			//Text = "ws://echo.websocket.org",
			//Text = "ws://homezigth.ddns.net:8888/ws",
			Text = "ws://192.168.199.111:8888/ws",
			//HorizontalOptions = LayoutOptions.Center
		};
		public static Button ConnectButton = new Button
		{
			Text = "Connect",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 1	
			//WidthRequest = 200
		};
		public static ActivityIndicator activityIndicator = new ActivityIndicator
		{
			Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
			//IsRunning = true,
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};
		public static Entry username = new Entry { Text = "" }; 
		public static Entry password = new Entry 
		{ 
			Text = "" ,
			IsPassword = true
		};
		public static Button loginButton = new Button 
		{ 
			Text = "Login",
			IsEnabled = false
		};
		public static Button logoutButton = new Button 
		{ 
			Text = "Logout",
			IsEnabled = false
		};

		public LoginPage ()
		{
			Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					new Label { Text = "Server Url", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)) }, 
					websocketUrl,
					ConnectButton,
					new Label { Text = "Login", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)) }, 
					new Label { Text = "Username" },
					username,
					new Label { Text = "Password" },
					password,
					new StackLayout
					{

						//VerticalOptions = LayoutOptions.Center,
						//Orientation = StackOrientation.Horizontal,
						//HorizontalOptions = LayoutOptions.CenterAndExpand,
						Children = 
						{
							loginButton,
							logoutButton
						}
					},
					activityIndicator

				}
			};
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			foreach (var data in await App.Database.Get_flag_Login()) 
			{
				username.Text = data.username;
				password.Text = data.password;
				username.IsEnabled = false;
				password.IsEnabled = false;
				loginButton.IsEnabled = false;
				logoutButton.IsEnabled = true;
			}
		}

	}

}

