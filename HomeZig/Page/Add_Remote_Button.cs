using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Add_Remote_Button : ContentPage
	{
		public Add_Remote_Button ()
		{
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry ();
			nameLabel.FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label));
			nameEntry.Placeholder = "Type name of remote button";

		}
	}
}

