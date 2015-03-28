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
		static App ap = null;
		static MenuTabPage mt = null;
		public static IPageManager ipm;
		static DeviceItemDatabase dvi = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Log.Debug ("OnCreate", "OnCreateeeeeeeeee");
			Xamarin.Forms.Forms.Init (this, bundle);
			ToastNotificatorImplementation.Init();

			dvi = new DeviceItemDatabase ();
			App.Database.Delete_RemoteData_Item ();
			App.Database.Delete_All_Login_Username_Show_For_Del ();

			ipm = this;
			ap = new App();
			LoadApplication (ap);
			mt = new MenuTabPage (ipm);
				




			//ap = new App();


			//App.Navigation = page.Navigation;
			//SetPage(page);
			//SetPage (App.GetMainPage ());
			//LoadApplication (page);
			//this.RunOnUiThread (() => {new AllDevice("ssss");} );
			//new DeviceItemDatabase ();
			//new ConnectClick (this);

		}

		protected override void OnStart()
		{
			Log.Debug ("OnStart", "OnStart called, App is Active");
			base.OnStart();

			try
			{			
				if( WebsocketManager.websocketMaster.State == WebSocketState.Open) {					
					LoadApplication (mt);
				}else{

				}
				Log.Debug ("OnResume", "WebsocketManager.websocketMaster.Opened !!!");
			}
			catch
			{

				Log.Debug ("OnResume", "WebsocketManager.websocketMaster.State CATCH!!!");
			}

		}

		protected override void OnPause()
		{
			Log.Debug ("OnPause", "OnPause called, App is moving to background");
			base.OnPause();
		}
		protected override void OnStop()
		{			
			Log.Debug ("OnStop", "OnStop called, App is in the background");
			base.OnStop();
		}

		protected override void OnResume()
		{			
			base.OnResume();
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			//WebsocketManager.websocketMaster.Dispose ();

			//base.OnPause();
			//try
			//{
				//LoginPage.ConnectButton.IsEnabled = true;
				//WebsocketManager.websocketMaster.Close ();
			//}
			//catch{
				Log.Debug ("OnDestroy", "OnDestroy called, App is Terminating");
			//}

		}

		public void showLoginPage ()
		{		//SetPage (App.GetLoginPage ());
			//ap = new App();
			//LoadApplication (ap);	 

		}

		public void showLoginPageDis ()
		{
			//SetPage (App.GetLoginPage ());
			WebsocketManager.websocketMaster.Close ();
			//WebsocketManager.websocketMaster.Dispose ();
			//LoginPage.ConnectButton.IsEnabled = true;
			//ap.Dispose();
			//ap = new App();
			LoadApplication (ap);

			
			//ap = new App ();
			//LoadApplication (ap);
			 
		}
		public void showMenuTabPage (IPageManager ipm)
		{
			//App.Navigation.PushAsync(new MenuTabPage(ipm));
			//mt = new MenuTabPage(ipm);
			LoadApplication (mt);
			Login_Page_Action.nws = null;
			Log.Info ("prevent_other_change_page" ,"GGGGGGGGGGGGGGGGG");
			//LoadApplication (new MenuTabPage(ipm));
		}

		public void showHomePage()
		{
			//SetPage (App.GetMainPage ()); 
		}

	}


}

