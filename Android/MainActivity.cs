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
using Connectivity.Plugin;
using Toasts.Forms.Plugin.Abstractions;

namespace HomeZig.Android
{
	[Activity (Label = "HomeZig.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		//static App ap = null;
		//static MenuTabPage mt = null;
		//public static IPageManager ipm;
		//static DeviceItemDatabase dvi = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Log.Debug ("OnCreate", "OnCreateeeeeeeeee");
			Forms.Init (this, bundle);
			ToastNotificatorImplementation.Init();

			WebsocketManager.getService = this;
			new DeviceItemDatabase ();
			new InitializePage ();
			App.Database.Delete_RemoteData_Item ();
			App.Database.Delete_All_Login_Username_Show_For_Del ();

			//ipm = this;
			//ap = new App();
			//LoadApplication (ap);
			//mt = new MenuTabPage (ipm);				

			CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
			{				
				if(!args.IsConnected){
					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (async () => {
							var notificator = DependencyService.Get<IToastNotificator>();
							await notificator.Notify(ToastNotificationType.Warning, 
								"No Internet Connection", "Check your internet connection", TimeSpan.FromSeconds(10));							
						});

						LoginPage.activityIndicator.IsRunning = false;
						LoginPage.ConnectButton.IsEnabled = true;

					})).Start();
					//LoadApplication (ap);
				}else{
					//LoadApplication (ap);
				}
			};

			LoadApplication (new App());


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
					//LoadApplication (mt);
					App.current.showMenuTabPage();
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



	}


}

