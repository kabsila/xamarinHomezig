using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_GpdPage : ContentPage
	{
		
		public static Db_allnode item;
		public static NameByUser NBitem;
		public static ListView ioListView;
		//public static bool doSwitch = false;
		Label NameOfNode;

		public Node_io_GpdPage ()
		{
			System.Diagnostics.Debug.WriteLine ("= new Node_io_GpdPage ()");
			ioListView = new ListView ();
			ioListView.ItemTemplate = new DataTemplate(typeof (Node_io_Gpd_Cell));
			//ioListView.IsEnabled = false;
			NameOfNode = new Label
			{
				Text = "Name of node", // Change in OnAppearing
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
		
			Button test = new Button 
			{
				Text = "Test GPD"
			};
			test.Clicked += DependencyService.Get<I_Node_io_Gpd> ().testClick;

			MessagingCenter.Subscribe<ContentPage, NameByUser> (new ContentPage(), "Node_io_Gpd_EditActionClicked", (sender, arg) => {
				var DeviceList = new Node_io_Gpd_Edit ();
				DeviceList.BindingContext = arg;
				Navigation.PushAsync (DeviceList);
			});

			var layout = new StackLayout 
			{
				Children = 
				{
					NameOfNode,
					test,
					//tableView
					ioListView
				}
			};
			Content = layout;


		}

		/**async void setSwitchIo(string node_addr)
		{
			foreach (var s in await App.Database.GetIoOfNode(node_addr)) {
				if (s.node_addr == item.node_addr) {
					SetSwitchByNodeIo (s.node_io);
					break;
				}
			}
		}
		void SetSwitchByNodeIo(string io)
		{
			string state = NumberConversion.hex2binary (io);
			string state01 = state.Substring(4, 1);
			string state02 = state.Substring(5, 1);
			string state03 = state.Substring(6, 1);
			string state04 = state.Substring(7, 1);

			if (state01.Equals ("0")){ 
				state01 = "false";
			} else {
				state01 = "true";
			}

			if (state02.Equals ("0")){ 
				state02 = "false";
			} else {
				state02 = "true";
			}

			if (state03.Equals ("0")){ 
				state03 = "false";
			} else {
				state03 = "true";
			}

			if (state04.Equals ("0")){ 
				state04 = "false";
			} else {
				state04 = "true";
			}

			switchCell_01.On = Convert.ToBoolean(state01);
			switchCell_02.On = Convert.ToBoolean(state02);
			switchCell_03.On = Convert.ToBoolean(state03);
			switchCell_04.On = Convert.ToBoolean(state04);
			//doSwitch = true;

		}**/

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			if (Config.addr.Equals (string.Empty))
			{
				item = (Db_allnode)BindingContext;
				NameOfNode.Text = item.name_by_user;
				//SetSwitchByNodeIo (item.node_io);
				ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr (item.node_addr);
			} else 
			{			
				NameOfNode.Text = Config.nameAddr;	
				ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr (Config.addr);
				Config.addr = string.Empty;
				Config.nameAddr = string.Empty;
			}

		}

		protected override void OnDisappearing ()
		{			
			MessagingCenter.Unsubscribe<Node_io_GpdPage, string> (this, "Node_io_Gpd_Change_Detected");
			base.OnDisappearing ();
		}


	}

}

