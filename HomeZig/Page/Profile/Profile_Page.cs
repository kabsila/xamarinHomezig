﻿using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_Page : ContentPage
	{
		ListView ProfileListview;
		public static string profileName = string.Empty;
		public Profile_Page ()
		{
			ProfileListview = new ListView ();
			ProfileListview.ItemTemplate = new DataTemplate(typeof (Profile_Cell));

			Button addProfile = new Button 
			{
				Text = "Add Profile",
			};

			//addProfile.Clicked += DependencyService.Get<I_Profile> ().addProfile_Click;
			addProfile.Clicked += async (sender, e) =>
			{
				Page page = (Page)Activator.CreateInstance(typeof (Add_Profile_Page));
				await this.Navigation.PushAsync(page);
			};

			MessagingCenter.Subscribe<ContentPage, ProfileData> (new ContentPage(), "Profile_Name_EditActionClicked", (sender, arg) => {
				var profileList = new Edit_Profile_Name_Page ();
				profileList.BindingContext = arg;
				Navigation.PushAsync (profileList);
			});

			ProfileListview.ItemTapped += (sender, e) => 
			{
				var itemData = (ProfileData)e.Item;
				var DeviceList = new Profile_Setting_Page ();
				DeviceList.BindingContext = itemData;
				Navigation.PushAsync (DeviceList);
				//System.Diagnostics.Debug.WriteLine(itemData.profileName);
			};

			var layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				Children = 
				{
					addProfile,
					ProfileListview
				}
			};
			Content = layout;
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			//item = (Db_allnode)BindingContext;
			//NameOfNode.Text = item.name_by_user;
			//SetSwitchByNodeIo (item.node_io);
			ProfileListview.ItemsSource = await App.Database.Get_ProfileName_GroupBy_Addr();
		}
	}
}

