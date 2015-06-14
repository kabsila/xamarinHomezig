using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace HomeZig
{
	public class Add_Profile_Cell : ViewCell
	{
		public Add_Profile_Cell ()
		{
			var label = new Label 
			{
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			label.SetBinding (Label.TextProperty, "node_name_by_user");

			Switch sw = new Switch ();

			//sw.Toggled += DependencyService.Get<I_Add_Profile> ().switcher_Toggled;
			sw.Toggled += switcher_Toggled;

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

		public void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			var b = (Switch)sender;
			var ItemData = (NameByUser)b.BindingContext;

			ProfileData ProfileData = new ProfileData ();

			if (e.Value) {				
				ProfileData.NameByUserNodeOfProfile = ItemData.node_name_by_user;
				ProfileData.nodeAddrOfProfile = ItemData.node_addr;
				ProfileData.node_deviceType = ItemData.node_deviceType;
				Add_Profile_Page.ProfileDataList.Add (ProfileData);
			} else {
				Add_Profile_Page.ProfileDataList.RemoveAll (x => x.nodeAddrOfProfile == ItemData.node_addr);
			}

		}

		protected override void OnBindingContextChanged ()
		{			
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

