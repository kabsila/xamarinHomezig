using System;
using Xamarin.Forms;
using System.Threading.Tasks;




namespace HomeZig
{
	public class HomePage : ContentPage
	{


		public static Button ConnectButton = new Button
		{
			Text = "Connect",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 1	,
			WidthRequest = 200
		};

		public static Button Disconnectbutton = new Button
		{
			Text = "Disconnect",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 1,
			WidthRequest = 200

		};

		public static Editor wsUrlEditor = new Editor
		{
			Text = "ws://echo.websocket.org",
			//Text = "ws://homezigth.ddns.net:8888/ws",
			//Text = "ws://101.51.28.48:8888/ws",
			HorizontalOptions = LayoutOptions.Center
		};

		public static ActivityIndicator activityIndicator = new ActivityIndicator
		{
			Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
			//IsRunning = true,
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		public HomePage()
		{
			//button1.SetBinding (Button.TextProperty, "Name");
			Label header = new Label
			{
				Text = "Entry",
				Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.Center
			};

		/**	Button button1 = new Button
			{
				Text = " Go to Label Demo Page ",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1
			};**/


			
			this.Title = "HomePage";
			this.Content = new StackLayout
			{

				Spacing = 10,
				VerticalOptions = LayoutOptions.Start,
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Center,

				Children = 
				{
					header,
					wsUrlEditor,
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Start,
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.Center,
						Children = 
						{
							ConnectButton,
							Disconnectbutton,
						}
					},
					activityIndicator

				}
			};
		}




	}
}

