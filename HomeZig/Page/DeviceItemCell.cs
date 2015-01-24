using System;
using Xamarin.Forms;



namespace HomeZig
{
	public class DeviceItemCell : ViewCell
	{
		//DeviceItemCell pageAction;


		public DeviceItemCell ()
		{
			var label = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			label.SetBinding (Label.TextProperty, "node_addr");

			var swCell = new Switch
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			swCell.SetBinding(Switch.IsToggledProperty, "nodeStatusToString");
			swCell.Toggled += DependencyService.Get<IDeviceItemCell> ().switcher_Toggled;

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{

					label,
					swCell
					//tick
				}
			};
			View = layout;

			//DependencyService.Get<IDeviceItemCell> ().DeviceItemCellGetAction ();
		}

		protected override void OnBindingContextChanged ()
		{
			// Fixme : this is happening because the View.Parent is getting 
			// set after the Cell gets the binding context set on it. Then it is inheriting
			// the parents binding context.
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}

		/**void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Switch is now {0}", e.Value);
		}**/
	}
}

