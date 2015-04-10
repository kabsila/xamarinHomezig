using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Delete_Remote_Page : ContentPage
	{
		public static ListView remoteButtonListName;
		public static Label deleteStatus = new Label
		{
			Text = "Tap Item For Delete"
		};
		public Delete_Remote_Page ()
		{
			remoteButtonListName = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			remoteButtonListName.ItemTemplate = new DataTemplate(typeof (TextCell));
			remoteButtonListName.ItemTemplate.SetBinding (TextCell.TextProperty, "remote_button_name");

			remoteButtonListName.ItemTapped += DependencyService.Get<I_Delete_Remote> ().deleteRemote_Tapped;

			this.Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					deleteStatus,
					remoteButtonListName,
				}
			};
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			//item = (Db_allnode)BindingContext;
			deleteStatus.TextColor = Color.Default;
			//deleteStatus.Text = "Tap Item For Delete";
			deleteStatus.Text = "จิ้มเพื่อลบ";
			remoteButtonListName.ItemsSource = await App.Database.Get_RemoteData_Item();
		}
	}
}

