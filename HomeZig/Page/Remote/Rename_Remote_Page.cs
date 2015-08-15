using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Rename_Remote_Page : ContentPage
	{
		public static RemoteData item;

		public static Entry RemoteName_value = new Entry 
		{ 
			Text = "" ,
			Placeholder = "Type new name of remote button"
		}; 

		public Rename_Remote_Page ()
		{
			

			Button renameSubmitButton = new Button
			{
				Text = "Submit" 
			};

			Button cancelRemoteButton = new Button { Text = "Cancel" };

			ActivityIndicator AddRemoteIndicator = new ActivityIndicator
			{
				Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
				//IsRunning = true,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			//public static StackLayout addRemotePageLayout = new StackLayout();

		/**	Label plsWaitText = new Label 
			{
				Text = "Add remote command to remoteGateway node",
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};**/

			Label renameRemoteButtonHeader = new Label 
			{
				Text = "Rename remote command",
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			Label instructionRemoteName_Label = new Label 
			{
				Text = "Name",
			};

			//renameSubmitButton.Clicked -= DependencyService.Get<I_Node_io_RemoteControl> ().submitRenameButton_Click;
			//renameSubmitButton.Clicked += DependencyService.Get<I_Node_io_RemoteControl> ().submitRenameButton_Click;

			Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.StartAndExpand,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					renameRemoteButtonHeader,
					instructionRemoteName_Label,
					RemoteName_value,
					renameSubmitButton,
					cancelRemoteButton,					
					AddRemoteIndicator
				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			item = (RemoteData)BindingContext;

			RemoteName_value.Text = item.remote_button_name;

			//Add_Remote_Single_Page.plsWaitText.BackgroundColor = Color.Default;
			//Add_Remote_Single_Page.plsWaitText.Text = "";
			//instructionRemoteName_value.Text = "";

		}
	}
}

