using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Powered : ContentPage
	{
		public static Button button3 = new Button
		{
			Text = " Power",
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

		public Powered ()
		{
			ToolbarItem Edit = new ToolbarItem
			{
				Text = "Edit",
				Order = ToolbarItemOrder.Primary
			};

			Edit.Clicked += (sender, args) =>
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

