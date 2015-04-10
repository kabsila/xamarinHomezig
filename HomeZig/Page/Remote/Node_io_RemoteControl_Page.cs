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
			Text = "ADD Single"
		};

		public static Button addDoubleControl = new Button 
		{
			Text = "ADD Double"
		};

		public static Button addTripleControl = new Button 
		{
			Text = "ADD Triple"
		};

		public static Button DeleteControl = new Button 
		{
			Text = "Delete"
		};

		public Node_io_RemoteControl_Page ()
		{

			remoteButtonListName = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			remoteButtonListName.ItemTemplate = new DataTemplate(typeof (TextCell));
			remoteButtonListName.ItemTemplate.SetBinding (TextCell.TextProperty, "remote_button_name");

			remoteButtonListName.ItemTapped += DependencyService.Get<I_Node_io_RemoteControl> ().NodeIoRemoteControl_Tapped;

			addSingleControl.Clicked += addSingleControl_Click;
			addDoubleControl.Clicked += addDoubleControl_Click;
			addTripleControl.Clicked += addTripleControl_Click;
			DeleteControl.Clicked += DeleteControl_Click;

			var layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				VerticalOptions = LayoutOptions.Start,
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {
					addSingleControl,
					addDoubleControl,
					addTripleControl,
					DeleteControl,
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

		void addDoubleControl_Click(object sender, EventArgs e)
		{
			var DeviceList = new Add_Remote_Double_Page ();
			DeviceList.BindingContext = item;
			Navigation.PushAsync (DeviceList);
		}

		void addTripleControl_Click(object sender, EventArgs e)
		{
			var DeviceList = new Add_Remote_Triple_Page ();
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

