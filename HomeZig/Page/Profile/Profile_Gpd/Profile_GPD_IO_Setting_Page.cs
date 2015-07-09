using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_GPD_IO_Setting_Page : ContentPage
	{		
		ListView ProfileGpdIoListView;
		Label NameOfNode;
		string nodeAddr = string.Empty;
		SwitchCell alert_mode;
		public Profile_GPD_IO_Setting_Page ()
		{
			var IOLabel = new Label { Text = "Setting Detector IO Profile" };

			ProfileGpdIoListView = new ListView ();
			ProfileGpdIoListView.ItemTemplate = new DataTemplate(typeof (Profile_GPD_IO_Setting_Cell));


			NameOfNode = new Label
			{				
				Text = "Name of node", // Change in OnAppearing
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};

			ContentView cv = new ContentView
			{
				Content = NameOfNode,
				Padding = new Thickness(0, 10, 0, 0)
			};
			alert_mode = new SwitchCell
			{
				Text = "Security Mode",
			};

			alert_mode.OnChanged += async (sender, e) => 
			{
				var secureMode = Convert.ToString(e.Value);//.ToString();
				System.Diagnostics.Debug.WriteLine("{0}", secureMode);
				await App.Database.Update_Profile_IO_Data_SecurityMode(Profile_Page.profileName, nodeAddr, secureMode);
			};
			Button test = new Button {Text = "TEST"};
			test.Clicked += async (sender, e) => 
			{
				foreach(var data in await App.Database.Get_Profile_IO_Data())
				{
					System.Diagnostics.Debug.WriteLine("profileName = {0} node_addr = {1} io_value = {2} node_io_p = {3} alert_mode = {4}",data.profileName,data.node_addr,data.io_value,data.node_io_p,data.alert_mode);
				}	
			};

			TableView alert_mode_tableView = new TableView
			{				
				//Intent = TableIntent.Form,
				Root = new TableRoot
				{					
					new TableSection(" ")
					{						
						alert_mode,
					}
				}
			};

			var layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				//Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = 
				{
					IOLabel,
					//NameOfNode,
					cv,
					alert_mode_tableView,
					test,
					//ProfileGpdIoListView,

				}
				};			
			Content = layout;
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			var item = (ProfileData)BindingContext;
			NameOfNode.Text = item.NameByUserNodeOfProfile;
			nodeAddr = item.nodeAddrOfProfile;
			/**var alert_mode = "0";
			nodeAddr = item.nodeAddrOfProfile;
			var tempData = await App.Database.Get_NameByUser_by_addr(item.nodeAddrOfProfile);
			foreach (var data in tempData)
			{
				await App.Database.Insert_Profile_IO_Data (Profile_Page.profileName, data.node_addr, data.node_io_p, data.io_value, alert_mode, data.io_name_by_user, data.node_deviceType);
			}**/

			foreach(var data in await App.Database.Get_Profile_IO_Data_By_Addr(item.nodeAddrOfProfile, Profile_Page.profileName))
			{
				alert_mode.On = Convert.ToBoolean(data.alert_mode);
				break;
			}


			//ProfileGpdIoListView.ItemsSource = await App.Database.Get_Profile_IO_Data_By_Addr(item.nodeAddrOfProfile, Profile_Page.profileName);
		}

		protected override void OnDisappearing ()
		{			
			base.OnDisappearing ();
		}
	}
}

