using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_Item_Cell : ViewCell
	{
		public Node_io_Item_Cell ()
		{
			var label = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			label.SetBinding (Label.TextProperty, "io_name_by_user");

			var label2 = new Label {
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			//label2.SetBinding (Label.BackgroundColorProperty, );

			Switch sw = new Switch ();

			sw.SetBinding (Switch.IsToggledProperty, "io_value");
			sw.IsEnabled = false;
			//sw.Toggled += DependencyService.Get<IDeviceCall> ().switcher_Toggled;



			var layout = new StackLayout {
				Padding = new Thickness(30, 0, 30, 0),
				Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{

					label,
					label2,
					//sw
				}
			};
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

