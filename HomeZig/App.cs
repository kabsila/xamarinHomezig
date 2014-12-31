using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class App
	{
		public static INavigation Navigation 
		{
			get;
			set;
		}

		public static Page GetMainPage ()
		{	
			return new NavigationPage(new HomePage());
		}
	}
}

