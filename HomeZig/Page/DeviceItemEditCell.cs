using System;
using Xamarin.Forms;
namespace HomeZig
{
	public class DeviceItemEditCell :  ViewCell
	{
		public DeviceItemEditCell ()
		{
			Label NameByUser = new Label
			{
				Text = "",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			NameByUser.SetBinding(Label.TextProperty, "name_by_user");
			Label EditName = new Label
			{
				Text = "Edit",
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,

			};

			var layout = new StackLayout 
			{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				Children = 
				{
					NameByUser,
					EditName
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

