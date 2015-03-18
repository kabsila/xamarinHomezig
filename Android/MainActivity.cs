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


namespace HomeZig.Android
{
	[Activity (Label = "HomeZig.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity, IPageManager
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			//var page = App.GetMainPage();

			new DeviceItemDatabase ();
			var page =  App.GetLoginPage ();
			SetPage(page);
			/**App.Check_flag_Login ();
			MessagingCenter.Subscribe<ContentPage> (this, "FlagLoginPass", (sender) => 
			{
					var page =  App.GetLoginPage ();
					SetPage(page);
			});
			MessagingCenter.Subscribe<ContentPage> (this, "FlagLoginNotPass", (sender) => 
			{
					var page =  App.GetLoginPage ();
					SetPage(page);
			});**/

			//App.Navigation = page.Navigation;
			//SetPage(page);
			//SetPage (App.GetMainPage ());
			//LoadApplication (page);
			//this.RunOnUiThread (() => {new AllDevice("ssss");} );
			//new DeviceItemDatabase ();
			//new ConnectClick (this);
			new LoginClick (this);

		}

		public void showMenuTabPage ()
		{
			SetPage (App.GetListMainPage ()); 
		}

		public void showHomePage()
		{
			SetPage (App.GetMainPage ()); 
		}

	}


}

