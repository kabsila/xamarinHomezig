using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceItemPage : ContentPage
	{
		public DeviceItemPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry ();
			//nameEntry.SetBinding (Entry.TextProperty, "Name");

			var deviceTypeLabel = new Label { Text = "DeviceType" };
			var deviceTypeEntry = new Entry ();
			//deviceTypeEntry.SetBinding (Entry.TextProperty, "DeviceType");

			var addressLabel = new Label { Text = "Address" };
			var addressEntry = new Switch ();
			//addressEntry.SetBinding (Entry.TextProperty, "Address");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) => {
				var todoItem = (Db_allnode)BindingContext;
				App.Database.Save_DBAllNode_Item(todoItem);
				this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) => {
				var todoItem = (Db_allnode)BindingContext;
				App.Database.DeleteItem(todoItem.ID);
				this.Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				var todoItem = (Db_allnode)BindingContext;
				this.Navigation.PopAsync();
			};


			/**var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				var todoItem = (DeviceDatabaseTable)BindingContext;
				DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
			};**/

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					deviceTypeLabel, deviceTypeEntry,
					addressLabel, addressEntry,
					saveButton, deleteButton, cancelButton,
					//speakButton
				}
			};
		}
	}
}

