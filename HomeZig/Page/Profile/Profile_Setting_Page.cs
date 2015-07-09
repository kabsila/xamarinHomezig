using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_Setting_Page : ContentPage
	{
		ListView listOfNodeProfile;
		public Profile_Setting_Page ()
		{
			var nameLabel = new Label { Text = "Setting Profile" };
			listOfNodeProfile = new ListView ();
			listOfNodeProfile.ItemTemplate = new DataTemplate(typeof (Profile_Setting_Cell));

			listOfNodeProfile.ItemTapped += (sender, e) => 
			{
				var itemData = (ProfileData)e.Item;

				if(itemData.node_deviceType.Equals(EnumtoString.EnumString(DeviceType.InWallSwitch))){
					var DeviceList = new Profile_Inwall_IO_Setting_Page ();
					DeviceList.BindingContext = itemData;
					Navigation.PushAsync (DeviceList);
				}else if(itemData.node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
					var DeviceList = new Profile_GPD_IO_Setting_Page ();
					DeviceList.BindingContext = itemData;
					Navigation.PushAsync (DeviceList);
				}

			};

			Content = new StackLayout {
				Padding = new Thickness(40, 10, 40, 10),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					nameLabel,
					listOfNodeProfile
				}
			};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing ();
			var item = (ProfileData)BindingContext;
			Profile_Page.profileName = item.profileName;
			listOfNodeProfile.ItemsSource = await App.Database.Get_Node_For_Profile(item.profileName);

			var addr = await App.Database.Get_Addr_Of_ProfileName (item.profileName);
			var alert_mode = "False";
			foreach (var dataAddr in addr)
			{
				var tempData = await App.Database.Get_NameByUser_by_addr(dataAddr.nodeAddrOfProfile);
				foreach (var data in tempData)
				{
					await App.Database.Update_IO_Name (data.io_name_by_user, data.node_io_p, data.node_addr);
					await App.Database.Insert_Profile_IO_Data (Profile_Page.profileName, data.node_addr, data.node_io_p, data.io_value, alert_mode, data.io_name_by_user, data.node_deviceType);
				}
			}


			/**System.Diagnostics.Debug.WriteLine ("nodeAddrOfProfile =>" + item.nodeAddrOfProfile);
			var alert_mode = "0";
			var tempData = await App.Database.Get_NameByUser_by_addr(item.nodeAddrOfProfile);
			foreach (var data in tempData)
			{
				await App.Database.Insert_Profile_IO_Data (Profile_Page.profileName, data.node_addr, data.node_io_p, data.io_value, alert_mode, data.io_name_by_user, data.node_deviceType);
			}**/

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();

		}
	}
}

