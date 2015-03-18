using System;
using Xamarin.Forms;

namespace HomeZig
{
	public interface I_Admin_Delete_User
	{
		void queryUser (object sender, EventArgs e);
		void userForDelete_Tapped(object sender, ItemTappedEventArgs e);
	}
}

