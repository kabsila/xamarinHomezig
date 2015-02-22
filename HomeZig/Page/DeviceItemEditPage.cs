using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceItemEditPage : ContentPage
	{
		public DeviceItemEditPage ()
		{
			//this.SetBinding (ContentPage.TitleProperty, "Name");

			//NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry ();
			nameLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));
			nameEntry.Placeholder = "Type name of node for yourself";
			nameEntry.SetBinding (Entry.TextProperty, "name_by_user");

			var addrLabel = new Label { Text = "Node ID" };
			var addrEntry = new Label ();
			addrEntry.SetBinding (Label.TextProperty, "node_addr");
			addrLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				var todoItem = (Db_allnode)BindingContext;
				todoItem.name_by_user = nameEntry.Text;
				await App.Database.Update_Node_NameByUser(todoItem.name_by_user, todoItem.node_addr);
				await Navigation.PopAsync();
				//System.Diagnostics.Debug.WriteLine("kkkkkkkkkkkkkkkkkkkk {0}", todoItem.name_by_user);
				//MessagingCenter.Send<ContentPage> (new ContentPage(), "BackFromEdit");
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) => {
				var todoItem = (Db_allnode)BindingContext;
				await Navigation.PopAsync();
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					addrLabel, addrEntry,
					saveButton, cancelButton
				}
			};
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();
			MessagingCenter.Send<ContentPage> (new ContentPage(), "BackFromEdit");
		}
	}
}

