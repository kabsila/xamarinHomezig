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
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
				//FontAttributes = FontAttributes.Bold 
				VerticalOptions = LayoutOptions.CenterAndExpand,
				//HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			label1.SetBinding(Label.TextProperty, "name_by_user");

			var EditAction = new MenuItem { Text = "Edit" }; // red background
			EditAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			EditAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (Db_allnode)mi.BindingContext;
				MessagingCenter.Send<ContentPage, Db_allnode> (new ContentPage(), "DeviceAddressList_EditActionClicked", mo);

				System.Diagnostics.Debug.WriteLine("DeviceAddressList_EditActionClicked");
			};

			//
			// add context actions to the cell
			//
			//ContextActions.Add (moreAction);
			ContextActions.Add (EditAction);



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

