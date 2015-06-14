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
			//DeviceAddressListPage.addressListView.IsRefreshing = true;kkk
			Db_allnode requestRefresh = new Db_allnode();
			requestRefresh.ID = 0;
			requestRefresh.name_by_user = "";
			requestRefresh.node_addr = "";
			requestRefresh.node_io = "";
			requestRefresh.node_io_p = "";
			requestRefresh.node_status = "";
			requestRefresh.node_type = "0x3ff11";
			requestRefresh.node_command = "is_alive_node_type";

			//string jsonCommand = "{\"cmd_db_allnode\":[{\"node_io\": \"FF\", \"node_type\": \"0x3ff11\", \"node_addr\": \"[00:13:a2:00:40:ad:bd:8c]!\", \"node_status\": \"1\", \"node_command\": \"listview_request\"}, {\"node_io\": \"0f\", \"node_type\": \"0x3ff11\", \"node_addr\": \"[00:13:a2:00:40:ad:bd:30]!\", \"node_status\": \"1\", \"node_command\": \"listview_request\"}]}\n";
			string jsonCommand = "{\"cmd_db_allnode\":[{\"node_io\": \"FF\", \"node_type\": \"0x3ff11\", \"node_addr\": \"[00:13:a2:00:40:ad:58:kk]!\", \"node_status\": \"0\", \"node_command\": \"listview_request\"}, {\"node_io\": \"0f\", \"node_type\": \"0x3ff11\", \"node_addr\": \"[00:13:a2:00:40:ad:bd:30]!\", \"node_status\": \"0\", \"node_command\": \"listview_request\"}]}\n";

			//string jsonCommand = JsonConvert.SerializeObject (requestRefresh, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine ("jsonCommandRefresh" , jsonCommand);
			WebsocketManager.websocketMaster.Send (jsonCommand);
		}

		//{"cmd_db_allnode":[{"node_io": "FF", "node_type": "0x3ff11", "node_addr": "[00:13:a2:00:40:ad:bd:8c]!", "node_status": "1", "node_command": "listview_request"}, {"node_io": "0f", "node_type": "0x3ff11", "node_addr": "[00:13:a2:00:40:ad:bd:30]!", "node_status": "0", "node_command": "listview_request"}]}

	}
}

