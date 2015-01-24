using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Reflection;
using Android.Util;



namespace HomeZig.Android
{
	public class AllDeviveLoad : AllDevicePage
	{
		//public AllDeviveLoad (List<Db_allnode> obj)
		public AllDeviveLoad (List<CmdDbAllnode> obj)
		{

			var deviceType = new List<string>{ };
			foreach (PropertyInfo p in typeof(CmdDbAllnode).GetProperties())
			{
				//string propertyName = p.Name;
				deviceType.Add (p.Name);
			}


			/**for (int i = 0; i < obj.Count; i++) {
				if (obj [i].node_type.Equals ("0x3ff90")) {
					deviceType.Add ("Outlet");
				} else if (obj [i].node_type.Equals ("0xa001a")) {
					deviceType.Add ("Camera");
				} else if (obj [i].node_type.Equals ("0x0")) {
					deviceType.Add ("Gateway");
				} else {
					deviceType.Add ("Unknow");
				}
			}**/		

			//listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			//listView.ItemTemplate.SetBinding(TextCell.TextProperty, "node_addr");	

			AllDeviceListView.ItemsSource = deviceType;
			AllDeviceListView.ItemSelected += (sender, e) => {
				//var eq = e.SelectedItem;
				//DisplayAlert("Earthquake info", eq.ToString(), "OK", "Cancel");
				var listView2 = new ListView
				{
					HasUnevenRows = true
				
				};
				try
				{
					listView2.ItemTemplate = new DataTemplate(typeof(SwitchCell));
					//listView2.ItemTemplate.SetBinding(TextCell.TextProperty, new Binding("node_addr"));
					listView2.ItemTemplate.SetBinding(SwitchCell.TextProperty, "node_addr");
					listView2.ItemTemplate.SetBinding(SwitchCell.OnProperty, "nodeStatusToString");

					switch (e.SelectedItem.ToString())
					{
						case "Outlet":
						listView2.ItemsSource = obj[0].Outlet;
						break;

						case "Camera":
						listView2.ItemsSource = obj[0].Camera;
						break;
					}

				}catch (Exception ex)
				{
					Log.Info ("MessageReceived" , ex.Message);
				}

				Page a = new ContentPage
				{
					Content = new StackLayout
					{
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children = { listView2 }
					}
				};

				App.Navigation.PushAsync(a);


			};

			var listView3 = new ListView
			{
				HasUnevenRows = true

			};
			listView3.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView3.ItemTemplate.SetBinding(TextCell.TextProperty, new Binding("node_addr"));
			Page aa = new ContentPage
			{
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					Children = { listView3 }
				}
			};

			ToolbarItem addNewItem = new ToolbarItem
			{
				Text = "Edit",
				Order = ToolbarItemOrder.Default
			};

			addNewItem.Activated += (sender, args) =>
			{
				Log.Info("toolbar","toolbar");
				this.Navigation.PushAsync(new Outlet2());

			};
			this.ToolbarItems.Add(addNewItem);
		}


	}
}

