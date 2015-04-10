using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Node_io_Gpd_Cell : ViewCell
	{
		public Node_io_Gpd_Cell ()
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

			var EditAction = new MenuItem { Text = "Edit", IsDestructive = true }; // red background
			EditAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			EditAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (NameByUser)mi.BindingContext;

				MessagingCenter.Send<ContentPage, NameByUser> (new ContentPage(), "Node_io_Gpd_EditActionClicked", mo);
				//var DeviceList = new Node_io_Item_Edit ();
				//DeviceList.BindingContext = mo;
				//Node_io_ItemPage.Navigation.PushAsync (DeviceList);
				//System.Diagnostics.Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
				System.Diagnostics.Debug.WriteLine("Edit Context Action clicked: " + mo.io_name_by_user);
			};

			//
			// add context actions to the cell
			//

			ContextActions.Add (EditAction);

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

		protected override void OnBindingContextChanged ()
		{			
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

