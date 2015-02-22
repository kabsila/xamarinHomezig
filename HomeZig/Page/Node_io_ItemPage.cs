using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_ItemPage : ContentPage
	{
		static SwitchCell switchCellLeft; 
		static SwitchCell switchCellRight;
		public static Db_allnode item;
		public static bool doSwitch = false;
		Label NameOfNode;
		public Node_io_ItemPage ()
		{
			NameOfNode = new Label
			{
				Text = "Name of node",
				FontAttributes = FontAttributes.Bold,
				FontSize = 40,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};


			/**Button test = new Button 
			{
				Text = "Test"
			};
			test.Clicked += DependencyService.Get<IDeviceCall> ().testClick;**/

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
			};
			var layout = new StackLayout 
			{
				//Padding = new Thickness(0, 0, 0, 0),
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					NameOfNode,
					//test,
					tableView
				}
			};
			Content = layout;

			MessagingCenter.Subscribe<ContentPage> (this, "ChangeSwitch", (sender) => 
			{
				doSwitch = false;
				item = (Db_allnode)BindingContext;
				Device.BeginInvokeOnMainThread (() => {
						setSwitchIo(item.node_addr);
						//switchCellLeft.SetValue(SwitchCell.OnProperty,false);
				});
				
			});
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
			switchCellLeft.On = Convert.ToBoolean(stateLeft);
			switchCellRight.On = Convert.ToBoolean(stateRight);
			doSwitch = true;

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;
			NameOfNode.Text = item.name_by_user;
			SetSwitchByNodeIo (item.node_io);
			//addressListView.ItemsSource = await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
		}

	}
}

