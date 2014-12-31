using System;

using Xamarin.Forms;

namespace HomeZig
{
	public class Outlet : ContentPage
	{
		public static Button button3 = new Button
		{
			Text = " button3 ",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 20,
			BorderRadius = 5,
		};

		public static Button connect = new Button
		{
			Text = " connect ",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 1	,
		};

		public Outlet ()
		{
			this.Content = new StackLayout
			{

				Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Center,

				Children = 
				{

					button3,
					connect
				}
			};
		}
	}
}

