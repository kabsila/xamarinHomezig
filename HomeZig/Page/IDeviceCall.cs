using System;
using Xamarin.Forms;

namespace HomeZig
{
	public interface IDeviceCall
	{				
		void switcher_Toggled(object sender, ItemTappedEventArgs e);
		void switchLeft_OnChange(object sender, ToggledEventArgs e);
		void switchRight_OnChange(object sender, ToggledEventArgs e);
		void testClick(object sender, EventArgs e);
	}
}

