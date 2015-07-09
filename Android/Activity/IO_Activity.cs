using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace HomeZig.Android
{
	[Activity(Label = "IO Activity")]
	public class IO_Activity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//string dataFromNotify = Intent.Extras.GetString (DataActivity.activityNameAddr);

			//int count = Intent.Extras.GetInt ("count", -1);
			//if (count <= 0) {
			//	return;
			//}
			Config.nameAddr = Intent.Extras.GetString (DataActivity.activityNameAddr);
			Config.addr = Intent.Extras.GetString (DataActivity.activityAddr);

			LoadApplication (new App(new Node_io_GpdPage()));
			//LoadApplication (new App(dataFromNotify));
			//(App.Current.MainPage as NavigationPage).PushAsync(new Node_io_GpdPage());
			//App.Current.MainPage.Navigation.PushAsync(new Node_io_GpdPage());
			//Console.WriteLine (Config.activityData);
		}

		public override void OnBackPressed()
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(intent);
		}

	}


}

