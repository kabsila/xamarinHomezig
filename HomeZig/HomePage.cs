﻿using System;
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
			HorizontalOptions = LayoutOptions.Center
		};
		public HomePage()
		{
			//button1.SetBinding (Button.TextProperty, "Name");
			Label header = new Label
			{
				Text = "Entry",
				Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
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
							Disconnectbutton
						}
					}

				}
			};
		}




	}
}

