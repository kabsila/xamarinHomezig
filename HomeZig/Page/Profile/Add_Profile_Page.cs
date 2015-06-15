using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace HomeZig
{	
	public class Add_Profile_Page : ContentPage
	{
		ListView deviceListview;
		public static List<ProfileData> ProfileDataList = new List<ProfileData> ();
		public Add_Profile_Page ()
		{
			var nameLabel = new Label { Text = "Profile Name" };
			var nameEntry = new Entry ();
			nameLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));
			nameEntry.Placeholder = "Type name of profile";
			nameEntry.SetBinding (Entry.TextProperty, "profileName");

			deviceListview = new ListView ();
			deviceListview.ItemTemplate = new DataTemplate(typeof (Add_Profile_Cell));


			deviceListview.ItemTapped += (sender, e) => 
			{
				var Item = (NameByUser)e.Item;
				System.Diagnostics.Debug.WriteLine("node_addr => " + Item.node_addr);
				//((ListView)sender).se
			};

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				if (String.IsNullOrEmpty(nameEntry.Text) || !ProfileDataList.Any()) //check list is not null
				{
					await DisplayAlert("Validation Error", "Profile name are required or Node not selected", "Re-try");
				} 
				else 
				{
					foreach(var item in ProfileDataList)
					{
						item.profileName = nameEntry.Text;
						item.profile_status = "False";
					}

					//await App.Database.Insert_ProfileData_Item(nameEntry.Text);
					await App.Database.Insert_ProfileData_Item(ProfileDataList);
					await Navigation.PopAsync();
					ProfileDataList.Clear();
				}
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) => {
				
				ProfileDataList.Clear();
				var todoItem = (ProfileData)BindingContext;
				await Navigation.PopAsync();
			};

			Content = new StackLayout {
				Padding = new Thickness(30, 10, 30, 10),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					nameLabel, nameEntry, deviceListview,
					saveButton, cancelButton
				}
			};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing ();
			deviceListview.ItemsSource = await App.Database.Get_NameByUser_GroupBy_Addr();

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();

		}
	}
}

