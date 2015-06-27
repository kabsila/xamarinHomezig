using System;
using Xamarin.Forms;
using HomeZig.iOS;
using WebSocket4Net;
using System.Text;


[assembly: Dependency (typeof (Option_Page_Action))]
namespace HomeZig.iOS
{
	public class Option_Page_Action : IOption
	{
		public Option_Page_Action ()
		{
		}

		public void logOut()
		{
			WebsocketManager.websocketMaster.Close ();
			new System.Threading.Thread (new System.Threading.ThreadStart (() => {
				Device.BeginInvokeOnMainThread (() => {
					App.current.showLoginPage ();
				});
			})).Start();

		}
	}
}

