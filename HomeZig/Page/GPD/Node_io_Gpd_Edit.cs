using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_Gpd_Edit : ContentPage
	{
		public Node_io_Gpd_Edit ()
		{
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry ();
			nameLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));
			nameEntry.Placeholder = "Type name of node for yourself";
			nameEntry.SetBinding (Entry.TextProperty, "io_name_by_user");

			var addrLabel = new Label { Text = "Node ID" };
			var addrEntry = new Label ();
			addrEntry.SetBinding (Label.TextProperty, "node_addr");
			addrLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				var todoItem = (NameByUser)BindingContext;	
				todoItem.io_name_by_user = nameEntry.Text;
				await App.Database.Update_NameByUser_by_target_io(nameEntry.Text, todoItem.node_addr, todoItem.node_io_p);
				await Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) => {
				//var todoItem = (Db_allnode)BindingContext;
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
			//MessagingCenter.Send<ContentPage> (new ContentPage(), "BackFromEdit");
		}
	}
}

