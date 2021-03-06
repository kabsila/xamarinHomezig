﻿using System;
using Xamarin.Forms;

namespace HomeZig
{
	
	public class Profile_IO_Setting_Page : ContentPage
	{	
		StackLayout layout;
		ListView ProfileIoListView;
		Label NameOfNode;

		public Profile_IO_Setting_Page ()
		{
			var IOLabel = new Label { Text = "Setting IO Profile" };

			ProfileIoListView = new ListView ();
			ProfileIoListView.ItemTemplate = new DataTemplate(typeof (Profile_IO_Setting_Cell));

			NameOfNode = new Label
			{				
				Text = "Name of node", // Change in OnAppearing
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			SwitchCell alert_mode = new SwitchCell
			{
				Text = "Security Mode",
			};

			/**Button test = new Button {Text = "TEST"};
			test.Clicked += async (sender, e) => 
			{
				foreach(var data in await App.Database.Get_Profile_IO_Data())
				{
					System.Diagnostics.Debug.WriteLine("profileName = {0} node_addr = {1} io_value = {2} node_io_p = {3} alert_mode = {4}",data.profileName,data.node_addr,data.io_value,data.node_io_p,data.alert_mode);
				}	
			};**/

			TableView alert_mode_tableView = new TableView
			{				
				///Intent = TableIntent.Form,
				Root = new TableRoot
				{			
					new TableSection
					{						
						alert_mode,
					}
				}
			};

			layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				//Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = 
				{
					IOLabel,
					NameOfNode,
					alert_mode_tableView,
					//test,
					ProfileIoListView,

				}
			};			
			Content = layout;
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			var item = (ProfileData)BindingContext;
			NameOfNode.Text = item.NameByUserNodeOfProfile;

			//Content = lay
			/**var alert_mode = "0";
			var tempData = await App.Database.Get_NameByUser_by_addr(item.nodeAddrOfProfile);
			foreach (var data in tempData)
			{
				await App.Database.Insert_Profile_IO_Data (Profile_Page.profileName, data.node_addr, data.node_io_p, data.io_value, alert_mode, data.io_name_by_user, data.node_deviceType);
			}**/
			ProfileIoListView.ItemsSource = await App.Database.Get_Profile_IO_Data_By_Addr(item.nodeAddrOfProfile, Profile_Page.profileName);
		}

		protected override void OnDisappearing ()
		{			
			base.OnDisappearing ();
		}
	}
}

