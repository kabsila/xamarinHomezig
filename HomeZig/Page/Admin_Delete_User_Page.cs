using System;
using Xamarin.Forms;
using System.Collections.Generic;


namespace HomeZig
{
	public class Admin_Delete_User_Page : ContentPage
	{
		public static ListView usernameForDelete;
		//public static List<Login> ListOfusernameForDelete = new List<Login>();

		public Admin_Delete_User_Page ()
		{
			usernameForDelete = new ListView();
			usernameForDelete.ItemTemplate = new DataTemplate(typeof (TextCell));
			usernameForDelete.ItemTemplate.SetBinding (TextCell.TextProperty, "username");


			usernameForDelete.ItemTapped += DependencyService.Get<I_Admin_Delete_User> ().userForDelete_Tapped;

			Button queryButton = new Button
			{
				Text = "List of Username",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1	
			};
			//queryButton.Clicked += DependencyService.Get<I_Admin_Delete_User> ().queryUser;

			Label deleteUserHeader = new Label 
			{
				Text = "Delete User",
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			//DependencyService.Get<I_Admin_Delete_User> ().queryUser;

			this.Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					deleteUserHeader,
					//queryButton,
					usernameForDelete
				}
			};
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			usernameForDelete.ItemsSource = await App.Database.Get_Login_Username_Show_For_Del();
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//await App.Database.Delete_All_Login_Username_Show_For_Del ();
		}
	}
}

