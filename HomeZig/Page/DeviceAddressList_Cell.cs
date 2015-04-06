using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace HomeZig
{
	public class DeviceAddressList_Cell : ViewCell
	{
		public DeviceAddressList_Cell ()
		{
			var label1 = new Label 
			{ 
				//Text = "Label 1", 
				//FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
				//FontAttributes = FontAttributes.Bold 
				VerticalOptions = LayoutOptions.CenterAndExpand,
				//HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			label1.SetBinding(Label.TextProperty, "name_by_user");

			var moreAction = new MenuItem { Text = "More" };
			moreAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			moreAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
			};

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			deleteAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
			};

			//
			// add context actions to the cell
			//
			ContextActions.Add (moreAction);
			ContextActions.Add (deleteAction);



			View = new StackLayout {
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness (15, 0, 0, 0),
				Children = {label1
					//new StackLayout {
					//	Orientation = StackOrientation.Vertical,
						//Children = { label1 }
					//}
				}
			};
		}
	}
}

