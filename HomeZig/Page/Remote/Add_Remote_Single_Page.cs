using System;
using Xamarin.Forms;

namespace HomeZig
{
	public class Add_Remote_Single_Page : ContentPage
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

		//public static StackLayout addRemotePageLayout = new StackLayout();

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

		public Add_Remote_Single_Page ()
		{

			addRemoteSubmitButton.Clicked -= DependencyService.Get<I_Add_Remote_Single> ().submitRemoteButton_Click;
			addRemoteSubmitButton.Clicked += DependencyService.Get<I_Add_Remote_Single> ().submitRemoteButton_Click;

			cancelRemoteButton.Clicked += DependencyService.Get<I_Add_Remote_Single> ().cancelRemoteButton_Click;
			/**saveButton.Clicked += async (sender, e) => {
				//var todoItem = (Db_allnode)BindingContext;
				//todoItem.name_by_user = nameEntry.Text;
				//await App.Database.Update_Node_NameByUser(todoItem.name_by_user, todoItem.node_addr);
				//await Navigation.PopAsync();
				RemoteData rd = new RemoteData();
				rd.ID = 0;
				rd.node_addr = item.node_addr;
				rd.remote_button_name = instructionRemoteName_value.Text;
				rd.remote_code = "";
				//string jsonCommandLogin = JsonConvert.SerializeObject(data, Formatting.Indented);
				//WebsocketManager.websocketMaster.Send (jsonCommandLogin.ToString());
			};**/


			/**cancelButton.Clicked += async (sender, e) => {
				//var todoItem = (Db_allnode)BindingContext;
				//await Navigation.PopAsync();
			};**/


			/**addRemotePageLayout.Padding = new Thickness (40, 40, 40, 10);
			addRemotePageLayout.VerticalOptions = LayoutOptions.StartAndExpand;
			addRemotePageLayout.Children.Add (addRemoteButtonHeader);
			addRemotePageLayout.Children.Add (instructionRemoteName_Label);
			addRemotePageLayout.Children.Add (instructionRemoteName_value);
			addRemotePageLayout.Children.Add (addRemoteSubmitButton);
			addRemotePageLayout.Children.Add (cancelRemoteButton);
			addRemotePageLayout.Children.Add (AddRemoteIndicator);
			
			instructionRemoteName_value.Text = "";
			this.Content = addRemotePageLayout;**/
			
			Content = new StackLayout {
				Padding = new Thickness (40, 40, 40, 10),
				//Spacing = 10,
				VerticalOptions = LayoutOptions.StartAndExpand,
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.Center,
				Children = {
					addRemoteButtonHeader,
					instructionRemoteName_Label,
					instructionRemoteName_value,
				    addRemoteSubmitButton,
				    cancelRemoteButton,					
					AddRemoteIndicator
				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			item = (Db_allnode)BindingContext;

			Add_Remote_Single_Page.plsWaitText.BackgroundColor = Color.Default;
			Add_Remote_Single_Page.plsWaitText.Text = "";
			instructionRemoteName_value.Text = "";
			
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();
			/**Device.BeginInvokeOnMainThread (() => {
				addRemotePageLayout.Children.Remove (addRemoteButtonHeader);
				addRemotePageLayout.Children.Remove (instructionRemoteName_Label);
				addRemotePageLayout.Children.Remove (instructionRemoteName_value);
				addRemotePageLayout.Children.Remove (addRemoteSubmitButton);
				addRemotePageLayout.Children.Remove (cancelRemoteButton);
				addRemotePageLayout.Children.Remove (AddRemoteIndicator);
				addRemotePageLayout.Children.Remove (plsWaitText);

				addRemotePageLayout.Children.Remove (Add_Remote_Single_Page.plsWaitText);
				AddRemoteIndicator.IsRunning = false;
				addRemoteSubmitButton.IsEnabled = true;
			});**/

			//MessagingCenter.Send<ContentPage> (new ContentPage(), "BackFromEdit");
		}

	}
}

