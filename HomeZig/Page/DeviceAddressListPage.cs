using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceAddressListPage : ContentPage
	{
		ListView addressListView;
		public DeviceAddressListPage ()
		{
			//Title = "Powered";

			addressListView = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			addressListView.ItemTemplate = new DataTemplate(typeof (TextCell));
			addressListView.ItemTemplate.SetBinding(TextCell.TextProperty, "node_addr");
			//listView.ItemTemplate.SetBinding(SwitchCell.TextProperty, "node_addr");
			//listView.ItemTemplate.SetBinding(SwitchCell.OnProperty, "nodeStatusToString");

			addressListView.ItemSelected += (sender, e) => {
				var Item = (Db_allnode)e.SelectedItem;
				var DeviceList = new Node_io_ItemPage();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync(DeviceList);
			};

			var layout = new StackLayout();
			layout.Children.Add(addressListView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			#region toolbar
		/**	ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", () => {
					var todoItem = new Db_allnode();
					//var todoPage = new DeviceItemPage();
					//todoPage.BindingContext = todoItem;
					//Navigation.PushAsync(todoPage);
				}, 0, 0);
			}

			ToolbarItems.Add (tbi);**/
			#endregion
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			//listView.ItemsSource = await App.Database.GetItems ();
			var deviceType = (Db_allnode)BindingContext;
			addressListView.ItemsSource = await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
		}


	}
}

