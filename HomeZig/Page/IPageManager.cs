using System;
using Xamarin.Forms;
namespace HomeZig
{
	public interface IPageManager
	{
		void showMenuTabPage (IPageManager ipm);
		void showHomePage();
		void showLoginPage();
		void showLoginPageDis();
	}

}

