using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_RemoteControl_Page : ContentPage
	{
		public static Db_allnode item;
		Button addControl = new Button 
		{
			Text = "ADD"
		};

		public Node_io_RemoteControl_Page ()
		{

			addControl.Clicked += DependencyService.Get<IRemoteControlCall> ().tellServerForAddControl;

			var layout = new StackLayout 
			{
				Padding = new Thickness(40, 10, 40, 10),
				VerticalOptions = LayoutOptions.Start,
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,

			};
			layout.Children.Add (addControl);
			Content = layout;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;
		}
	}
}

