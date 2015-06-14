using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_GPD_IO_Setting_Cell : ViewCell
	{
		public Profile_GPD_IO_Setting_Cell ()
		{
			var label = new Label 
			{
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			label.SetBinding (Label.TextProperty, "io_name_by_user");

			Switch sw = new Switch ();
			sw.SetBinding (Switch.IsToggledProperty, "io_value");

			sw.Toggled += DependencyService.Get<I_Profile> ().switcher_Toggled;

			var layout = new StackLayout 
			{
				Padding = new Thickness(30, 0, 30, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					label,
					sw
				}
				};
			View = layout;
		}
	}
}

