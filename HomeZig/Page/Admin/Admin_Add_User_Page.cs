using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Admin_Add_User_Page : ContentPage
	{

		public static Entry username = new Entry { Text = "" }; 
		public static Entry password = new Entry 
		{ 
			Text = "" ,
			//IsPassword = true
		};

		public static ActivityIndicator adminAddUserIndicator = new ActivityIndicator
		{
			Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
			//IsRunning = true,
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		public Admin_Add_User_Page ()
		{
			Label addUserHeader = new Label 
			{
				Text = "Add new User",
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			Button RegisButton = new Button
			{
				Text = "Register",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1	
			};
			RegisButton.Clicked += DependencyService.Get<I_Admin_Add_User> ().registerButton_Click;

			Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					addUserHeader,
					//new Label { Text = "Login", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)) }, 
					new Label { Text = "Username" },
					username,
					new Label { Text = "Password" },
					password,
					RegisButton,
					adminAddUserIndicator

				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			username.Text = "";
			password.Text = "";
			username.BackgroundColor = Color.Default;
			password.BackgroundColor = Color.Default;
		}
	}
}

