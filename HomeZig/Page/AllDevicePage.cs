﻿using System;
using Xamarin.Forms;


namespace HomeZig
{
	public class AllDevicePage : ContentPage
	{
		public static ListView AllDeviceListView = new ListView
		{
			HasUnevenRows = true			
		};
		public AllDevicePage ()
		{
			this.Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { AllDeviceListView }
			};


		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			AllDeviceListView.ItemsSource = await App.Database.GetItems ();
		}
	}
}

