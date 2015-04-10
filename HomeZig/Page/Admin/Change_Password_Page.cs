using System;
using Xamarin.Forms;
namespace HomeZig
{
	public class Change_Password_Page : ContentPage
	{
		public static Entry newPassword = new Entry 
		{ 
			Text = "" ,
			//IsPassword = true
			Placeholder = "Type new password"
		};

		public static ActivityIndicator changePasswordIndicator = new ActivityIndicator
		{
			Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
			//IsRunning = true,
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		public Change_Password_Page ()
		{
			Label changePasswordHeader = new Label 
			{
				Text = "Change password",
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			Button changePasswordButton = new Button
			{
				Text = "Submit",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1	
			};
			changePasswordButton.Clicked += DependencyService.Get<I_Change_Password> ().changePasswordButton_Click;

			Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					changePasswordHeader,

					new Label { Text = "New Password" },
					newPassword,
					changePasswordButton,
					changePasswordIndicator

				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			newPassword.Text = "";
			newPassword.BackgroundColor = Color.Default;
		}
	}
}

