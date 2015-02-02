using System;
using Xamarin.Forms;



namespace HomeZig
{
	public class DeviceItemCell : ViewCell
	{
		//DeviceItemCell pageAction;

		public static Db_allnode NodeItem;

		public DeviceItemCell ()
		{

			var label = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			label.SetBinding (Label.TextProperty, "node_addr");
			//label.BindingContext = new Db_allnode ().node_addr;

			var swCell = new Switch
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};


			swCell.SetBinding(Switch.IsToggledProperty, "nodeStatusToString");
			swCell.Toggled += switcher_Toggled;
			swCell.Toggled += DependencyService.Get<IDeviceItemCell> ().switcher_Toggled;

			//DependencyService.Get<IDeviceItemCell> ().NodeItem2 = (Db_allnode)BindingContext;
			/**this.Tapped += async (sender, e) => 
			{
				var todoItem = (Db_allnode)BindingContext;
				System.Diagnostics.Debug.WriteLine("Switch is now6 {0}", todoItem.node_addr);
			};**/

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
			//NodeItem = (Db_allnode)BindingContext;

			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();

		}

		async void switcher_Toggled(object sender, ToggledEventArgs e)
		{


			//var todoPage = new DeviceItemPage();
			//todoPage.BindingContext = todoItem;
			//NodeItem = (Db_allnode)BindingContext;
			//var todoItem = (Db_allnode)BindingContext;
			try
			{
				NodeItem = (Db_allnode)BindingContext;
				NodeItem.node_status = e.Value.ToString();
				NodeItem.nodeStatusToString = e.Value.ToString();


				System.Diagnostics.Debug.WriteLine("Switch is now2 {0}", NodeItem.node_status);

				await App.Database.Update_DBAllNode_Item(NodeItem);
				//await App.Database.Update_DBAllNode_Item2 (e.Value.ToString(), DeviceItemCell.NodeItem.ID);

			}
			catch (Exception exx)
			{
				System.Diagnostics.Debug.WriteLine (exx.ToString());
			}	
			//System.Diagnostics.Debug.WriteLine("Switch is now2 {0}", NodeItem.node_status);

		}
	}
}

