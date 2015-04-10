using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Add_Remote_Triple_Page : ContentPage
	{
		public static Db_allnode item;

		public static Entry instructionRemoteName_value = new Entry 
		{ 
			Text = "" ,
			Placeholder = "Type name of remote button (Example: Volume Up)"
		}; 

		public static Button addRemoteSubmitButton = new Button
		{
			Text = "Submit" 
		};

		Button cancelRemoteButton = new Button { Text = "Cancel" };

		public static ActivityIndicator AddRemoteIndicator = new ActivityIndicator
		{
			Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
			//IsRunning = true,
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		public static StackLayout addRemotePageLayout = new StackLayout();

		public static Label plsWaitText = new Label 
		{
			Text = "Add remote command to remoteGateway node",
			FontAttributes = FontAttributes.Bold,
			FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.EndAndExpand
		};

		Label addRemoteButtonHeader = new Label 
		{
			Text = "Add new remote command",
			FontAttributes = FontAttributes.Bold,
			FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.EndAndExpand
		};

		Label instructionRemoteName_Label = new Label 
		{
			Text = "Name",
		};

		public Add_Remote_Triple_Page ()
		{
			addRemoteSubmitButton.Clicked += DependencyService.Get<I_Add_Remote_Triple> ().submitRemoteButton_Click;

			cancelRemoteButton.Clicked += DependencyService.Get<I_Add_Remote_Triple> ().cancelRemoteButton_Click;

			addRemotePageLayout.Padding = new Thickness (40, 40, 40, 10);
			addRemotePageLayout.VerticalOptions = LayoutOptions.StartAndExpand;
			addRemotePageLayout.Children.Add (addRemoteButtonHeader);
			addRemotePageLayout.Children.Add (instructionRemoteName_Label);
			addRemotePageLayout.Children.Add (instructionRemoteName_value);
			addRemotePageLayout.Children.Add (addRemoteSubmitButton);
			addRemotePageLayout.Children.Add (cancelRemoteButton);
			addRemotePageLayout.Children.Add (AddRemoteIndicator);

			instructionRemoteName_value.Text = "";
			this.Content = addRemotePageLayout;		
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;

			Add_Remote_Triple_Page.plsWaitText.BackgroundColor = Color.Default;
			Add_Remote_Triple_Page.plsWaitText.Text = "";
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();
			Device.BeginInvokeOnMainThread (() => {
				addRemotePageLayout.Children.Remove (addRemoteButtonHeader);
				addRemotePageLayout.Children.Remove (instructionRemoteName_Label);
				addRemotePageLayout.Children.Remove (instructionRemoteName_value);
				addRemotePageLayout.Children.Remove (addRemoteSubmitButton);
				addRemotePageLayout.Children.Remove (cancelRemoteButton);
				addRemotePageLayout.Children.Remove (AddRemoteIndicator);
				addRemotePageLayout.Children.Remove (plsWaitText);

				addRemotePageLayout.Children.Remove (Add_Remote_Triple_Page.plsWaitText);
				AddRemoteIndicator.IsRunning = false;
				addRemoteSubmitButton.IsEnabled = true;
			});

			//MessagingCenter.Send<ContentPage> (new ContentPage(), "BackFromEdit");
		}
	}
}

