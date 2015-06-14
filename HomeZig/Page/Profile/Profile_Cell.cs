using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_Cell : ViewCell
	{
		public Profile_Cell ()
		{
			var nameOfProfile = new Label 
			{ 
				//Text = "Label 1", 
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			nameOfProfile.SetBinding(Label.TextProperty, "profileName");

			Switch sw = new Switch ();
			sw.Toggled += DependencyService.Get<I_Profile> ().profile_Toggled;

			var EditAction = new MenuItem { Text = "Edit" }; // red background
			EditAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			EditAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (ProfileData)mi.BindingContext;
				MessagingCenter.Send<ContentPage, ProfileData> (new ContentPage(), "Profile_Name_EditActionClicked", mo);
				System.Diagnostics.Debug.WriteLine("Profile_Name_EditActionClicked");
			};

			//
			// add context actions to the cell
			//
			//ContextActions.Add (moreAction);
			ContextActions.Add (EditAction);



			View = new StackLayout {
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(30, 0, 30, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {
					nameOfProfile,
					sw
				}
			};
		}
	}
}

