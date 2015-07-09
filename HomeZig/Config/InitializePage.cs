using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class InitializePage : ContentPage
	{
		public static Node_io_GpdPage ni_gpd;
		public static DeviceAddressListPage da_l;
		//public static Admin_Add_User_Page aau;
		//public static Admin_Delete_User_Page aad;
		//public static Change_Password_Page cp;
		public static Node_io_ItemPage ni_iw;
		//public static Option_Page op;

		public InitializePage ()
		{
			ni_gpd = new Node_io_GpdPage ();
			da_l = new DeviceAddressListPage ();
			//aau = new Admin_Add_User_Page ();
			//aad = new Admin_Delete_User_Page ();
			//cp = new Change_Password_Page ();
			ni_iw = new Node_io_ItemPage ();
		}
	}
}

