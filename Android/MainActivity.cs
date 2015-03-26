using System;



using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gestures;
using Android.Util;

using WebSocket4Net;
using Newtonsoft.Json;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Threading.Tasks;
using Toasts.Forms.Plugin.Droid;

namespace HomeZig.Android
{
	[Activity (Label = "HomeZig.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity, IPageManager
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			ToastNotificatorImplementation.Init();
			//var page = App.GetMainPage();

			new DeviceItemDatabase ();
			App.Database.Delete_RemoteData_Item ();
			App.Database.Delete_All_Login_Username_Show_For_Del ();
			//var page =  App.GetLoginPage ();
			//SetPage(page);
			LoadApplication (new App());
			//App.Navigation = page.Navigation;
			//SetPage(page);
			//SetPage (App.GetMainPage ());
			//LoadApplication (page);
			//this.RunOnUiThread (() => {new AllDevice("ssss");} );
			//new DeviceItemDatabase ();
			//new ConnectClick (this);
			new LoginClick (this);
			new MenuTabPage (this);
		}

		public void showLoginPage ()
		{
			//SetPage (App.GetLoginPage ());
			LoadApplication(new App());
		}

		public void showMenuTabPage (IPageManager ipm)
		{
			//SetPage (App.GetListMainPage (ipm)); 
			LoadApplication (new MenuTabPage(ipm));
		}

		public void showHomePage()
		{
			//SetPage (App.GetMainPage ()); 
		}

	}


}

