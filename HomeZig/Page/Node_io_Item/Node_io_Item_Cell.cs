using System;
using Xamarin.Forms;

namespace HomeZig
{

	//public class ListButton : Button { }
	//public class Listsw : SwitchCell { }


	public class Node_io_Item_Cell : ViewCell
	{
		public Node_io_Item_Cell ()
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
			//sw.SetBinding (, "io_value");

			/**sw.Toggled += (sender, e) => {
				var b = (Switch)sender;
				var t = (NameByUser)b.BindingContext;
				//((ContentPage)((ListView)((StackLayout)b.ParentView).ParentView).ParentView).DisplayAlert("Clicked", t.target_io + " button was clicked", "OK");
				System.Diagnostics.Debug.WriteLine("clicked" + t.target_io);
			};**/

			sw.PropertyChanging += (sender, e) => 
			{
				//start prevent switch loopBle
				System.Diagnostics.Debug.WriteLine("PropertyChanging");
				if(Node_io_ItemPage.bindingChange){
					Node_io_ItemPage.doSwitch = false;
					Node_io_ItemPage.bindingChange = false;
				}else{
					Node_io_ItemPage.doSwitch = true;
				}
				//end prevent switch loopBle
			};
			sw.Toggled += DependencyService.Get<I_Node_io_Item> ().switcher_Toggled;


			var EditAction = new MenuItem { Text = "Edit", IsDestructive = true }; // red background
			EditAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			EditAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				var mo = (NameByUser)mi.BindingContext;

				MessagingCenter.Send<ContentPage, NameByUser> (new ContentPage(), "EditActionClicked", mo);
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
			///////prevent switch loopBle
			Node_io_ItemPage.doSwitch = false;
			Node_io_ItemPage.bindingChange = true;
			/////////////////////////////
			System.Diagnostics.Debug.WriteLine("OnBindingContextChanged");
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

