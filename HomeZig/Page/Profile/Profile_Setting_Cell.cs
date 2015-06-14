using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Profile_Setting_Cell : ViewCell
	{
		public Profile_Setting_Cell ()
		{			
			var label = new Label 
			{
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			label.SetBinding (Label.TextProperty, "NameByUserNodeOfProfile");

			var layout = new StackLayout 
			{
				Padding = new Thickness(30, 0, 30, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					label,
				}
			};
			View = layout;
		}

	}
}

