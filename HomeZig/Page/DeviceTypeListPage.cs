using System;
using Xamarin.Forms;
using System.Collections.Generic;
namespace HomeZig
{
	public class DeviceTypeListPage : ContentPage
	{
		ListView typeListView;
		public DeviceTypeListPage ()
		{
			typeListView = new ListView ();
			typeListView.ItemTemplate = new DataTemplate(typeof (TextCell));
			typeListView.ItemTemplate.SetBinding(TextCell.TextProperty, "node_deviceType");

			typeListView.ItemSelected += (sender, e) => {
				var Item = (Db_allnode)e.SelectedItem;
				var DeviceList = new DeviceAddressListPage();
				DeviceList.BindingContext = Item;
				Navigation.PushAsync(DeviceList);
			};

			var layout = new StackLayout();
			layout.Children.Add(typeListView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			#region toolbar
			/**	ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", () => {
					var todoItem = new Db_allnode();
					//var todoPage = new DeviceItemPage();
					//todoPage.BindingContext = todoItem;
					//Navigation.PushAsync(todoPage);
				}, 0, 0);
			}

			ToolbarItems.Add (tbi);**/
			#endregion
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			typeListView.ItemsSource = await App.Database.GetItemGroupByDeviceType ();
		}

	}
}

