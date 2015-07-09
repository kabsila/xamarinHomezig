using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceAddressListPage : ContentPage
	{

		public static ListView addressListView;
		//ToolbarItem Edit;
		public DeviceAddressListPage ()
		{
			//Title = "Powered";
			//NavigationPage.SetHasNavigationBar (this, true);
			//NavigationPage.SetBackButtonTitle (this., "back");
			addressListView = new ListView ();

			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			//addressListView.ItemTemplate = new DataTemplate(typeof (TextCell));
			addressListView.ItemTemplate = new DataTemplate(typeof (DeviceAddressList_Cell));
			//addressListView.ItemTemplate.SetBinding (TextCell.TextProperty, "name_by_user");
			//addressListView.IsRefreshing = true;
			addressListView.IsPullToRefreshEnabled = true;
			addressListView.Refreshing += DependencyService.Get<I_DeviceAddressList> ().refresh;

			addressListView.ItemTapped  += (sender, e) => 
			{	

				var Item = (Db_allnode)e.Item;
				if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.GeneralPurposeDetector)) && Item.node_status.Equals("0")) {
					var DeviceList = InitializePage.ni_gpd;//new Node_io_GpdPage ();
					DeviceList.BindingContext = Item;
					Navigation.PushAsync (DeviceList);
				} else if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.InWallSwitch)) && Item.node_status.Equals("0")) {
					var DeviceList = InitializePage.ni_iw;//new Node_io_ItemPage ();
					DeviceList.BindingContext = Item;
					Navigation.PushAsync (DeviceList);
				} else if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.RemoteControl)) && Item.node_status.Equals("0")) {
					var DeviceList = new Node_io_RemoteControl_Page ();
					DeviceList.BindingContext = Item;
					Navigation.PushAsync (DeviceList);
				} 
				else {
					((ListView)sender).SelectedItem = null; //disable listview hightLight
				}
			};
			MessagingCenter.Subscribe<ContentPage, Db_allnode> (new ContentPage(), "DeviceAddressList_EditActionClicked", (sender, arg) => {
				var DeviceList = new DeviceAddressList_Edit ();
				DeviceList.BindingContext = arg;
				Navigation.PushAsync (DeviceList);
			});

			/**addressListView.ItemSelected += (sender, e) => {
				var Item = (Db_allnode)e.SelectedItem;
				var DeviceList = new Node_io_ItemPage();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync(DeviceList);
			};**/
			//addressListView.ItemSelected += ItemClicked_1;

			/**Edit = new ToolbarItem
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
				addressListView.ItemTapped -= ItemClicked_1;
				addressListView.ItemTapped -= ItemClicked_Done;
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
				addressListView.ItemTapped -= ItemClicked_1;
				addressListView.ItemSelected -= ItemClicked_Edit;
				addressListView.ItemTapped += ItemClicked_Done;
			};**/

			/**var layout = new StackLayout();
			layout.Children.Add(addressListView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;**/

			Content = new StackLayout {				
				Padding = new Thickness (15, 30, 15, 0),
				Children = 
				{
					addressListView
				}
			};

		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			//listView.ItemsSource = await App.Database.GetItems ();
			//addressListView.ItemTapped  -= ItemClicked_1;
			//addressListView.ItemSelected -= ItemClicked_Edit;
			//addressListView.ItemTapped -= ItemClicked_Done;
			//addressListView.ItemTapped  += ItemClicked_1;
			/**MessagingCenter.Subscribe<ContentPage> (this, "BackFromEdit", (sender) => 
			{
				addressListView.ItemSelected -= ItemClicked_Edit;
				addressListView.ItemTapped -= ItemClicked_1;
				addressListView.ItemSelected += ItemClicked_Edit;
			});**/
			var deviceType = (Db_allnode)BindingContext;
			var dataSource =  await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());

			//var aa = await App.Database.Table_is_Emtry2 ();

			//System.Diagnostics.Debug.WriteLine ("AAAAAAAAAAAAAAAAA {0}",);
		

			foreach (var data in dataSource) 
			{
				if (data.name_by_user == null) {
					data.name_by_user = data.node_addr;
					await App.Database.Update_DBAllNode_Item(data);
				} 

				if (data.node_status.Equals ("1")) { // 1 is OFFLINE , 0 is ONLINE
					var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");
					if (indexForRemove == -1) {
						data.name_by_user = data.name_by_user + "(OffLine)";
					} else {
						data.name_by_user = data.name_by_user.Remove (indexForRemove);
						data.name_by_user = data.name_by_user + "(OffLine)";
					}
					await App.Database.Update_DBAllNode_Item (data);

				} else {
					var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");						
					if (indexForRemove != -1) {
						data.name_by_user = data.name_by_user.Remove (indexForRemove);
						await App.Database.Update_DBAllNode_Item (data);
					}
				}

				/**if (data.node_io.Equals ("FF") || data.node_io.Equals ("ff") || data.node_io.Equals (null)) {
					var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");
					if (indexForRemove == -1) {
						data.name_by_user = data.name_by_user + "(OffLine)";
					} else {
						data.name_by_user = data.name_by_user.Remove (indexForRemove);
						data.name_by_user = data.name_by_user + "(OffLine)";
					}

					await App.Database.Update_DBAllNode_Item (data);

				} else {
					var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");						
					if (indexForRemove != -1) {
						data.name_by_user = data.name_by_user.Remove (indexForRemove);
						await App.Database.Update_DBAllNode_Item (data);
					}

				}**/
			}
			addressListView.ItemsSource = dataSource;
		}

		void ItemClicked_1 (object sender, ItemTappedEventArgs e)
		{
			/**var Item = (Db_allnode)e.Item;
			if (Item.node_deviceType.Equals ("General purpose detector") && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_GpdPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else if (Item.node_deviceType.Equals ("In wall switch") && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_ItemPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else if (Item.node_deviceType.Equals ("Remote control") && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_RemoteControl_Page ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} 
			else {
				((ListView)sender).SelectedItem = null; //disable listview hightLight
			}**/

			var Item = (Db_allnode)e.Item;
			if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.GeneralPurposeDetector)) && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_GpdPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.InWallSwitch)) && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_ItemPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else if (Item.node_deviceType.Equals (EnumtoString.EnumString(DeviceType.RemoteControl)) && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_RemoteControl_Page ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} 
			else {
				((ListView)sender).SelectedItem = null; //disable listview hightLight
			}


		}

		/**void ItemClicked_Edit (object sender, SelectedItemChangedEventArgs e)
		{
			var Item = (Db_allnode)e.SelectedItem;
			var DeviceList = new DeviceItemEditPage();
			DeviceList.BindingContext = Item;
			Navigation.PushAsync(DeviceList);
		}

		void ItemClicked_Done (object sender, ItemTappedEventArgs e)
		{
			var Item = (Db_allnode)e.Item;
			if (Item.node_deviceType.Equals ("General purpose detector") && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_GpdPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else if (Item.node_deviceType.Equals ("In wall switch") && Item.node_status.Equals("0")) {
				var DeviceList = new Node_io_ItemPage ();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync (DeviceList);
			} else {
				((ListView)sender).SelectedItem = null;
			}
		}**/



	}
}

