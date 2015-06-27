using System;

using Xamarin.Forms;
using Toasts.Forms.Plugin.Abstractions;
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

			Edit.Clicked += (sender, args) =>
			{
				//Log.Info("toolbar","toolbar");
			};
			this.ToolbarItems.Add(Edit);

			connect.Clicked += (sender, e) => 
			{
				//var notificator = DependencyService.Get<IToastNotificator>();
				//bool tapped = await notificator.Notify(ToastNotificationType.Error, 
					//"Error", "Something went wrong", TimeSpan.FromSeconds(2));
			};
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

