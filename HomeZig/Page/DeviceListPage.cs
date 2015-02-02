using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceListPage : ContentPage
	{
		ListView listView;
		public DeviceListPage ()
		{
			Title = "Todo";

			listView = new ListView ();
			listView.ItemTemplate = new DataTemplate(typeof (DeviceItemCell));
			//listView.ItemTemplate.SetBinding(SwitchCell.TextProperty, "node_addr");
			//listView.ItemTemplate.SetBinding(SwitchCell.OnProperty, "nodeStatusToString");

			listView.ItemSelected += (sender, e) => {
				//var todoItem = (Db_allnode)e.SelectedItem;
				//var todoPage = new DeviceItemPage();
				//todoPage.BindingContext = todoItem;
				//Navigation.PushAsync(todoPage);
			};

			var layout = new StackLayout();
			layout.Children.Add(listView);
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
			listView.ItemsSource = await App.Database.GetItems ();
		}


	}
}

