using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_ItemPage : ContentPage
	{

		//static SwitchCell switchCellLeft; 
		//static SwitchCell switchCellRight;
		public static Db_allnode item;
		public static NameByUser NBitem;
		public static bool doSwitch = true;
		public static bool bindingChange = true;

		public static ListView ioListView;
		Label NameOfNode;
		//ToolbarItem Edit;
		public Node_io_ItemPage ()
		{

			ioListView = new ListView ();
			//listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			ioListView.ItemTemplate = new DataTemplate(typeof (Node_io_Item_Cell));
			//ioListView.ItemTemplate.SetBinding (, "io_value");
			//ioListView.ItemTemplate = new DataTemplate(typeof (SwitchCell));
			//ioListView.ItemTemplate.SetBinding (SwitchCell.TextProperty, "io_name_by_user");
			//ioListView.ItemTemplate.SetBinding (SwitchCell.OnProperty, "io_value");

			ioListView.ItemTapped += (sender, e) => 
			{
				((ListView)sender).SelectedItem = null;
			};

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
				Text = "Test"
			};
			test.Clicked += DependencyService.Get<I_Node_io_Item> ().testClick;

			MessagingCenter.Subscribe<ContentPage, NameByUser> (new ContentPage(), "EditActionClicked", (sender, arg) => {
				var DeviceList = new Node_io_Item_Edit ();
				DeviceList.BindingContext = arg;
				Navigation.PushAsync (DeviceList);
			});

			/**
			switchCellLeft = new SwitchCell
			{
				Text = "Left"
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			switchCellLeft.OnChanged += DependencyService.Get<IDeviceCall> ().switchLeft_OnChange;


			switchCellRight = new SwitchCell
			{
				Text = "Right"
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			switchCellRight.OnChanged += DependencyService.Get<IDeviceCall> ().switchRight_OnChange;

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
				{
					new TableSection
					{
						switchCellLeft,
						switchCellRight
					}
				}
			};**/
			var layout = new StackLayout 
			{
				//Padding = new Thickness(0, 0, 0, 0),
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					NameOfNode,
					test,
					//tableView
					ioListView
				}
			};
			Content = layout;
			//Content = ioListView;
		/**	Edit = new ToolbarItem
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

			Edit.Clicked += (sender, e) => 
			{
				this.ToolbarItems.Add(Done);
				this.ToolbarItems.Remove(Edit);
				var deviceType = (Db_allnode)BindingContext;
			};**/

			/**
			MessagingCenter.Subscribe<ContentPage> (this, "ChangeSwitchDetect", (sender) => 
			{
				doSwitch = false;
				item = (Db_allnode)BindingContext;
				Device.BeginInvokeOnMainThread (() => {
						setSwitchIo(item.node_addr);
				});
				
			});**/
		}

		async void setSwitchIo(string node_addr)
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
			string stateLeft = state.Substring(6, 1);
			string stateRight = state.Substring(7, 1);
			if (stateLeft.Equals ("0")) 
			{ 
				stateLeft = "false";
			} 
			else 
			{
				stateLeft = "true";
			}

			if (stateRight.Equals ("0")) 
			{ 
				stateRight = "false";
			} 
			else 
			{
				stateRight = "true";
			}
			//switchCellLeft.On = Convert.ToBoolean(stateLeft);
			//switchCellRight.On = Convert.ToBoolean(stateRight);
			doSwitch = true;

		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;
			NameOfNode.Text = item.name_by_user;
			//SetSwitchByNodeIo (item.node_io);

			ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr(item.node_addr);


		}

	}
}

