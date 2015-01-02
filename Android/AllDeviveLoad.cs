using System;
using System.Collections.Generic;

using Xamarin.Forms;



namespace HomeZig.Android
{
	public class AllDeviveLoad : AllDevicePage
	{
		public AllDeviveLoad (List<Db_allnode> obj)
		{
			var deviceType = new HashSet<string>();

			for (int i = 0; i < obj.Count; i++) {
				if (obj [i].node_type.Equals ("0x3ff90")) {
					deviceType.Add ("Outlet");
				} else if (obj [i].node_type.Equals ("0xa001a")) {
					deviceType.Add ("Camera");
				} else if (obj [i].node_type.Equals ("0x0")) {
					deviceType.Add ("Gateway");
				} else {
					deviceType.Add ("Unknow");
				}
			}		

			//listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			//listView.ItemTemplate.SetBinding(TextCell.TextProperty, "nodeTypeToString");
			listView.ItemsSource = deviceType;
			listView.ItemSelected += (sender, e) => {
				var eq = e.SelectedItem;
				DisplayAlert("Earthquake info", eq.ToString(), "OK", "Cancel");
			/**	var listView2 = new ListView
				{
					RowHeight = 100
				};
				//listView2.ItemTemplate = new DataTemplate(typeof(TextCell));
				//listView2.ItemTemplate.SetBinding(TextCell.TextProperty, "node_addr");
				listView2.ItemsSource = obj[0].node_addr;

				Page a = new ContentPage
				{
					Content = new StackLayout
					{
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children = { listView2 }
					}
				};

				App.Navigation.PushAsync(a);**/


			};
		}
	}
}

