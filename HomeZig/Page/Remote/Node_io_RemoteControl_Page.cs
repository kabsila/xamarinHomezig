using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_RemoteControl_Page : ContentPage
	{
		public static Db_allnode item;
		public static ListView remoteButtonListName;
		public static Button addSingleControl = new Button 
		{
			Text = "Add Remote Button"
		};

		/**public static Button addDoubleControl = new Button 
		{
			Text = "ADD Double"
		};

		public static Button addTripleControl = new Button 
		{
			Text = "ADD Triple"
		};**/

		public static Button DeleteControl = new Button 
		{
			Text = "Delete"
		};

		public Node_io_RemoteControl_Page ()
		{

			remoteButtonListName = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			remoteButtonListName.ItemTemplate = new DataTemplate(typeof (Node_io_RemoteControl_Cell));
			//remoteButtonListName.ItemTemplate.SetBinding (TextCell.TextProperty, "remote_button_name");

			remoteButtonListName.ItemTapped += DependencyService.Get<I_Node_io_RemoteControl> ().NodeIoRemoteControl_Tapped;

			addSingleControl.Clicked += addSingleControl_Click;

			//DeleteControl.Clicked += DeleteControl_Click;

			MessagingCenter.Subscribe<ContentPage, RemoteData> (new ContentPage(), "RenameRemote_Clicked", async (sender, arg) => {
				var renameRemote = new Rename_Remote_Page ();
				renameRemote.BindingContext = arg;
				//await Navigation.PushAsync (renameRemote);
				await Navigation.PushModalAsync(renameRemote);
			});


			var layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				VerticalOptions = LayoutOptions.Start,
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {
					addSingleControl,
					//DeleteControl,
					remoteButtonListName,
				}
			};
			//layout.Children.Add (addControl);
			//layout.Children.Add (remoteButtonListName);
			Content = layout;
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;
			remoteButtonListName.ItemsSource = await App.Database.Get_RemoteData_Item();
		}

		void addSingleControl_Click(object sender, EventArgs e)
		{
			var DeviceList = new Add_Remote_Single_Page ();
			DeviceList.BindingContext = item;
			Navigation.PushAsync (DeviceList);
		}

		void DeleteControl_Click(object sender, EventArgs e)
		{
			var DeviceList = new Delete_Remote_Page ();
			DeviceList.BindingContext = item;
			Navigation.PushAsync (DeviceList);
		}
	}
}

