using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class ListviewSwitchTemplate : ViewCell
	{
		public static Label header = new Label
		{
			Text = "SwitchCell",
			Font = Font.BoldSystemFontOfSize(NamedSize.Small),
			HorizontalOptions = LayoutOptions.Center
		};
		public ListviewSwitchTemplate ()
		{

			header.SetBinding(Label.TextProperty, new Binding("."));

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
				{
					new TableSection
					{
						new SwitchCell
						{
							Text = "SwitchCell:"
						}
					}
				}
			};
			tableView.SetBinding(SwitchCell.OnProperty, new Binding("."));

			//this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			// Build the page.
			this.View = new StackLayout
			{
				Children =
				{
					header,
					tableView
				}
				};
		}
	}
}

