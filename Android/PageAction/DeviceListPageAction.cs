using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;


[assembly: Dependency (typeof (DeviceAddressListPage_Action))]
namespace HomeZig.Android
{
	public class DeviceAddressListPage_Action : ContentPage, I_DeviceAddressList
	{
		public DeviceAddressListPage_Action ()
		{
		}

		public void refresh(object sender, EventArgs e)
		{
			//DeviceAddressListPage.addressListView.IsRefreshing = true;
			Db_allnode requestRefresh = new Db_allnode();
			requestRefresh.ID = 0;
			requestRefresh.name_by_user = "";
			requestRefresh.node_addr = "";
			requestRefresh.node_io = "";
			requestRefresh.node_io_p = "";
			requestRefresh.node_status = "";
			requestRefresh.node_type = "0x3ff11";
			requestRefresh.node_command = "is_alive_node_type";

			string jsonCommand = JsonConvert.SerializeObject (requestRefresh, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("jsonCommandRefresh" , jsonCommand);
			WebsocketManager.websocketMaster.Send (jsonCommand);
		}
	}
}

