using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Toasts;
using WebSocket4Net;
using Newtonsoft.Json;

using Connectivity.Plugin;
using System.Threading.Tasks;
//using Toasts.Forms.Plugin.iOS;
//using Toasts.Forms.Plugin.Abstractions;
using MessageBar;
namespace HomeZig.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public static App ap = null;

		//public static MenuTabPage mt = null;
		//public static IPageManager ipm;
		//static DeviceItemDatabase dvi = null;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			
			Forms.Init ();

			new DeviceItemDatabase ();
			App.Database.Delete_RemoteData_Item ();
			App.Database.Delete_All_Login_Username_Show_For_Del ();							

			CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
			{				
				if(!args.IsConnected){
					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {							
							MessageBarManager.SharedInstance.ShowMessage("No Internet Connection", "Check your internet connection", MessageType.Error);
						});

						LoginPage.activityIndicator.IsRunning = false;
						LoginPage.ConnectButton.IsEnabled = true;

					})).Start();
					//LoadApplication (new App());	

				}else{
					//LoadApplication (new App());

					MessageBarManager.SharedInstance.ShowMessage("Success", "This is success", MessageType.Success);

				}

			};
			LoadApplication (new App());

			if (options != null)
			{
				// check for a local notification
				if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null)
					{
						UIAlertController okayAlertController = UIAlertController.Create (localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
						okayAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
						//viewController.PresentViewController (okayAlertController, true, null);
						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
				);

				app.RegisterUserNotificationSettings (notificationSettings);
			}

			return base.FinishedLaunching (app, options);


		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			Console.WriteLine ("Alert notification");

			//ContentPage a = new ContentPage ();
			//Page a =  new Page();
			//a.DisplayAlert ("aa","vv","bb");
			//var action = await DisplayActionSheet ("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");

			// show an alert
			//UIAlertController okayAlertController = UIAlertController.Create (notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
			//okayAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
			//viewController.PresentViewController (okayAlertController, true, null);

			// reset our badge
			//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public override void OnActivated(UIApplication application)
		{
			
			base.OnActivated(application);

			try
			{			
				if( WebsocketManager.websocketMaster.State == WebSocketState.Open) {					
					App.current.showMenuTabPage();
				}else{

				}
				Console.WriteLine("WebsocketManager.websocketMaster.Opened !!!");

			}
			catch
			{
				Console.WriteLine("WWebsocketManager.websocketMaster.State CATCH!!!");
			}
		}

	}
}

