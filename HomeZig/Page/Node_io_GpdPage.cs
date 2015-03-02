using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_GpdPage : ContentPage
	{
		static SwitchCell switchCell_01; 
		static SwitchCell switchCell_02;
		static SwitchCell switchCell_03; 
		static SwitchCell switchCell_04;
		public static Db_allnode item;
		//public static bool doSwitch = false;
		Label NameOfNode;
		public Node_io_GpdPage ()
		{
			NameOfNode = new Label
			{
				Text = "Name of node",
				FontAttributes = FontAttributes.Bold,
				FontSize = 40,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};
		
			Button test = new Button 
			{
				Text = "Test"
			};
			test.Clicked += DependencyService.Get<IDeviceCall> ().testClick;

			switchCell_01 = new SwitchCell
			{
				Text = "01"				
			};
			//switchCell_01.OnChanged += DependencyService.Get<IGpd_Call> ().switch01_OnChange;

			switchCell_02 = new SwitchCell
			{
				Text = "02"				
			};
			//switchCell_02.OnChanged += DependencyService.Get<IGpd_Call> ().switch02_OnChange;

			switchCell_03 = new SwitchCell
			{
				Text = "03"				
			};
			//switchCell_03.OnChanged += DependencyService.Get<IGpd_Call> ().switch03_OnChange;

			switchCell_04 = new SwitchCell
			{
				Text = "04"				
			};
			//switchCell_04.OnChanged += DependencyService.Get<IGpd_Call> ().switch04_OnChange;

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
				{
					new TableSection
					{
						switchCell_01,
						switchCell_02,
						switchCell_03,
						switchCell_04
					}
				}
			};
			var layout = new StackLayout 
			{
				Children = 
				{
					NameOfNode,
					test,
					tableView
				}
			};
			Content = layout;

			MessagingCenter.Subscribe<ContentPage> (this, "ChangeSwitchDetect", (sender) => 
				{
					item = (Db_allnode)BindingContext;
					Device.BeginInvokeOnMainThread (() => {
						setSwitchIo(item.node_addr);
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

