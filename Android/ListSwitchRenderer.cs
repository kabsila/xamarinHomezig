using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HomeZig.Android;

[assembly: ExportRenderer (typeof (Switch), typeof (ListSwitchRenderer))]

namespace HomeZig.Android
{
	public class ListSwitchRenderer : SwitchRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Switch> e)
		{
			base.OnElementChanged (e);

			Control.Focusable = false;
		}
	}
}

