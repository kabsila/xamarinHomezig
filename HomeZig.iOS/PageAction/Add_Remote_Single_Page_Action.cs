using System;
using Xamarin.Forms;
using HomeZig.iOS;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Add_Remote_Single_Page_Action))]

namespace HomeZig.iOS
{
	public class Add_Remote_Single_Page_Action : ContentPage, I_Add_Remote_Single
	{
		public Add_Remote_Single_Page_Action ()
		{
		}

		public async void submitRemoteButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty (Add_Remote_Single_Page.instructionRemoteName_value.Text)) {
				await DisplayAlert ("Validation Error", "Remote name are required", "Re-try");
			}else{
				RemoteData rd = new RemoteData();
				foreach (var data in await App.Database.Get_flag_Login())
				{
					rd.remote_username = data.username;
					break;
				}

				rd.ID = 0;
				rd.node_addr = Add_Remote_Single_Page.item.node_addr;
				rd.remote_button_name = Add_Remote_Single_Page.instructionRemoteName_value.Text;
				rd.remote_code = "single";
				rd.node_command = "add_button_remote";
				string jsonCommandaddRemoteButton = JsonConvert.SerializeObject(rd, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine ("{0}",jsonCommandaddRemoteButton);
				WebsocketManager.websocketMaster.Send (jsonCommandaddRemoteButton.ToString());

				Device.BeginInvokeOnMainThread (() => {
					Add_Remote_Single_Page.addRemotePageLayout.Children.Add (Add_Remote_Single_Page.plsWaitText);
					Add_Remote_Single_Page.plsWaitText.TextColor = Color.Default;
					Add_Remote_Single_Page.plsWaitText.Text = "Push remote command";
					Add_Remote_Single_Page.AddRemoteIndicator.IsRunning = true;
					Add_Remote_Single_Page.addRemoteSubmitButton.IsEnabled = false;
				});
			}

		}

		public void cancelRemoteButton_Click(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread (() => {
				Add_Remote_Single_Page.addRemotePageLayout.Children.Remove (Add_Remote_Single_Page.plsWaitText);
				Add_Remote_Single_Page.AddRemoteIndicator.IsRunning = false;
				Add_Remote_Single_Page.addRemoteSubmitButton.IsEnabled = true;
			});
		}
	}
}

