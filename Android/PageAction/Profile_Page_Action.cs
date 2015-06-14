using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Linq;

[assembly: Dependency (typeof (Profile_Page_Action))]
namespace HomeZig.Android
{
	public class Profile_Page_Action : I_Profile
	{
		public Profile_Page_Action ()
		{
		}

		public async void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			var b = (Switch)sender;
			var IO_ProfileData = (Profile_IO_Data)b.BindingContext;
			var alert_mode = "0";
			//IO_ProfileData.io_value = Convert.ToByte (e.Value).ToString ();
			IO_ProfileData.io_value = e.Value.ToString();
			try
			{
				await App.Database.Update_Profile_IO_Data(Profile_Page.profileName, IO_ProfileData.node_addr, IO_ProfileData.node_io_p, IO_ProfileData.io_value, alert_mode);
				//await App.Database.Insert_Profile_IO_Data(Profile_Page.profileName, IO_ProfileData.node_addr, IO_ProfileData.node_io_p, IO_ProfileData.io_value, alert_mode);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex);
			}
		}

		public async void profile_Toggled(object sender, ToggledEventArgs e)
		{
			var b = (Switch)sender;
			var ProfileData = (ProfileData)b.BindingContext;
			if (e.Value) {
				var data = await App.Database.Get_Profile_IO_Data_By_PrpfileName (ProfileData.profileName);
				foreach(var item in data)
				{
					item.node_command = "Profile_Open";
				}
				string jsonProfile = JsonConvert.SerializeObject (data, Formatting.Indented);
				WebsocketManager.websocketMaster.Send (jsonProfile);
				System.Diagnostics.Debug.WriteLine ("jsonProfile", jsonProfile);
			} else {				
				var data = new ProfileData();
				data.node_command = "Profile_Close";
				data.profileName = ProfileData.profileName;
				string jsonProfile = JsonConvert.SerializeObject (data, Formatting.Indented);
				WebsocketManager.websocketMaster.Send (jsonProfile);
				System.Diagnostics.Debug.WriteLine ("jsonProfile", jsonProfile);
			}
		}

	}
}

