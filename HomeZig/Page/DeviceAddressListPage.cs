using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceAddressListPage : ContentPage
	{

		ListView addressListView;
		ToolbarItem Edit;
		public DeviceAddressListPage ()
		{
			//Title = "Powered";
			//NavigationPage.SetHasNavigationBar (this, true);
			//NavigationPage.SetBackButtonTitle (this., "back");
			addressListView = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			addressListView.ItemTemplate = new DataTemplate(typeof (TextCell));
			addressListView.ItemTemplate.SetBinding (TextCell.TextProperty, "name_by_user");
		

			/**addressListView.ItemSelected += (sender, e) => {
				var Item = (Db_allnode)e.SelectedItem;
				var DeviceList = new Node_io_ItemPage();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync(DeviceList);
			};**/
			//addressListView.ItemSelected += ItemClicked_1;

			Edit = new ToolbarItem
			{
				Text = "Edit",
				Order = ToolbarItemOrder.Primary
			};

			ToolbarItem Done = new ToolbarItem
			{
				Text = "Done",
				Order = ToolbarItemOrder.Primary
			};
			this.ToolbarItems.Add(Edit);

			Edit.Clicked += async (sender, e) => 
			{
				this.ToolbarItems.Add(Done);
				this.ToolbarItems.Remove(Edit);
				var deviceType = (Db_allnode)BindingContext;
				addressListView.ItemTemplate = new DataTemplate(typeof (DeviceItemEditCell));
				addressListView.ItemsSource = await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
				addressListView.ItemSelected -= ItemClicked_1;
				addressListView.ItemSelected -= ItemClicked_Done;
				addressListView.ItemSelected += ItemClicked_Edit;

			};

			Done.Clicked += async (sender, e) => 
			{
				this.ToolbarItems.Remove(Done);
				this.ToolbarItems.Add(Edit);
				var deviceType = (Db_allnode)BindingContext;
				addressListView.ItemTemplate = new DataTemplate(typeof (TextCell));
				addressListView.ItemTemplate.SetBinding(TextCell.TextProperty, "name_by_user");
				addressListView.ItemsSource = await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
				addressListView.ItemSelected -= ItemClicked_1;
				addressListView.ItemSelected -= ItemClicked_Edit;
				addressListView.ItemSelected += ItemClicked_Done;
			};

			var layout = new StackLayout();
			layout.Children.Add(addressListView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			//listView.ItemsSource = await App.Database.GetItems ();
			addressListView.ItemSelected -= ItemClicked_1;
			addressListView.ItemSelected -= ItemClicked_Edit;
			addressListView.ItemSelected -= ItemClicked_Done;
			addressListView.ItemSelected += ItemClicked_1;
			MessagingCenter.Subscribe<ContentPage> (this, "BackFromEdit", (sender) => 
			{
				addressListView.ItemSelected -= ItemClicked_Edit;
				addressListView.ItemSelected -= ItemClicked_1;
				addressListView.ItemSelected += ItemClicked_Edit;
			});
			var deviceType = (Db_allnode)BindingContext;
			var dataSource =  await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
			foreach (var data in dataSource) 
			{
				if (data.name_by_user == null) {
					data.name_by_user = data.node_addr;
					await App.Database.Update_DBAllNode_Item(data);
				} 
			}
			addressListView.ItemsSource = dataSource;
		}

		void ItemClicked_1 (object sender, SelectedItemChangedEventArgs e)
		{
			var Item = (Db_allnode)e.SelectedItem;
			var DeviceList = new Node_io_ItemPage();
			DeviceList.BindingContext = Item;
			Navigation.PushAsync(DeviceList);
		}

		void ItemClicked_Edit (object sender, SelectedItemChangedEventArgs e)
		{
			var Item = (Db_allnode)e.SelectedItem;
			var DeviceList = new DeviceItemEditPage();
			DeviceList.BindingContext = Item;
			Navigation.PushAsync(DeviceList);
		}

		void ItemClicked_Done (object sender, SelectedItemChangedEventArgs e)
		{
			var Item = (Db_allnode)e.SelectedItem;
			var DeviceList = new Node_io_ItemPage();
			DeviceList.BindingContext = Item;
			Navigation.PushAsync(DeviceList);
		}



	}
}

