using System;
using Xamarin.Forms;


namespace HomeZig
{
	public class AllDevicePage : ContentPage
	{
		public static ListView listView = new ListView
		{
			RowHeight = 100
		};
		public AllDevicePage ()
		{

			this.Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { listView }
			};


		}
	}
}

