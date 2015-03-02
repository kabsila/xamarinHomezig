using System;
using Xamarin.Forms;

namespace HomeZig
{
	public interface IGpd_Call
	{
		void switch01_OnChange(object sender, ToggledEventArgs e);
		void switch02_OnChange(object sender, ToggledEventArgs e);
		void switch03_OnChange(object sender, ToggledEventArgs e);
		void switch04_OnChange(object sender, ToggledEventArgs e);
		void testClick(object sender, EventArgs e);
	}
}

