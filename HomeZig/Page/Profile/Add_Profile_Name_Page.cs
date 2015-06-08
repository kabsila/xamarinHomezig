using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Add_Profile_Name_Page : ContentPage
	{
		public Add_Profile_Name_Page ()
		{
			var nameLabel = new Label { Text = "Profile Name" };
			var nameEntry = new Entry ();
			nameLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));
			nameEntry.Placeholder = "Type name of profile";
			nameEntry.SetBinding (Entry.TextProperty, "profileName");


			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {				
				await App.Database.Insert_ProfileData_Item(nameEntry.Text);
				await Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) => {
				var todoItem = (ProfileData)BindingContext;
				await Navigation.PopAsync();
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry,
					saveButton, cancelButton
				}
			};
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();

		}
	}
}

