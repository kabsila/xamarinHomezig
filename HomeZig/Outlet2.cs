using System;

using Xamarin.Forms;

namespace HomeZig
{
	public class Outlet2 : ContentPage
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

		public Outlet2 ()
		{
			ToolbarItem Edit = new ToolbarItem
			{
				Text = "Outlet2",
				Order = ToolbarItemOrder.Primary
			};

			Edit.Activated += (sender, args) =>
			{
				//Log.Info("toolbar","toolbar");
			};
			this.ToolbarItems.Add(Edit);

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

