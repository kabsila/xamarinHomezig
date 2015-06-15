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
			sw.SetBinding(Switch.IsToggledProperty, "profile_status");
			sw.Toggled += DependencyService.Get<I_Profile> ().profile_Toggled;

			sw.PropertyChanging += (sender, e) => 
			{
				//prevent switch loopBle
				if(Profile_Page.bindingChange){
					Profile_Page.preventLoop = false;
					Profile_Page.bindingChange = false;
				}else{
					Profile_Page.preventLoop = true;
				}
				//prevent switch loopBle

			};

			var EditAction = new MenuItem { Text = "Edit" }; // red background
			EditAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			EditAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (ProfileData)mi.BindingContext;
				MessagingCenter.Send<ContentPage, ProfileData> (new ContentPage(), "Profile_Name_EditActionClicked", mo);
				System.Diagnostics.Debug.WriteLine("Profile_Name_EditActionClicked");
			};

			var DeleteAction = new MenuItem { Text = "Delete" }; // red background
			DeleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			DeleteAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (ProfileData)mi.BindingContext;
				await App.Database.Delete_IO_Profile_By_ProfileName(mo.profileName);
				await App.Database.Delete_Profile_By_ProfileName(mo.profileName);
				MessagingCenter.Send<ContentPage, ProfileData> (new ContentPage(), "Profile_Name_DeleteActionClicked", mo);
			};

			//
			// add context actions to the cell
			//
			//ContextActions.Add (moreAction);
			ContextActions.Add (EditAction);
			ContextActions.Add (DeleteAction);



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

		protected override void OnBindingContextChanged ()
		{
			///////prevent switch loopBle
			Profile_Page.preventLoop = false;
			Profile_Page.bindingChange = true;
			/////////////////////////////

			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

