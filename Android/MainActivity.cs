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
//using System.Threading.Tasks;


namespace HomeZig.Android
{
	[Activity (Label = "HomeZig.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity, IPageManager
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			var page = App.GetMainPage();
			//App.Navigation = page.Navigation;
			SetPage(page);
			//SetPage (App.GetMainPage ());
			//LoadApplication (page);
			//this.RunOnUiThread (() => {new AllDevice("ssss");} );
			new ConnectClick (this);

		}

		public void showMenuTabPage ()
		{
			SetPage (App.GetListMainPage ()); 
		}

	}


}

