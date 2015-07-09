using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_GpdPageAction))]

namespace HomeZig.Android
{
	public class Node_io_GpdPageAction : I_Node_io_Gpd
	{
		public Node_io_GpdPageAction ()
		{
		}

		/**public async void switch01_OnChange(object sender, ToggledEventArgs e)
		{

				Node_io_GpdPage.item.node_command = "command_io";
				Node_io_GpdPage.item.node_io_p = "1";
				var aStringBuilder = new StringBuilder(NumberConversion.hex2binary (Node_io_GpdPage.item.node_io));
				aStringBuilder.Remove(4, 1);
				aStringBuilder.Insert(4, Convert.ToByte(e.Value).ToString());
				Node_io_GpdPage.item.node_io = NumberConversion.binary2hex(aStringBuilder.ToString());

				string jsonCommandIo = JsonConvert.SerializeObject(Node_io_GpdPage.item, Formatting.Indented);
				await App.Database.Update_DBAllNode_Item(Node_io_GpdPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);
				
		}

		public async void switch02_OnChange(object sender, ToggledEventArgs e)
		{

				Node_io_GpdPage.item.node_command = "command_io";
				Node_io_GpdPage.item.node_io_p = "0";
				var aStringBuilder = new StringBuilder (NumberConversion.hex2binary (Node_io_GpdPage.item.node_io));
				aStringBuilder.Remove (5, 1);
				aStringBuilder.Insert (5, Convert.ToByte (e.Value).ToString ());
				Node_io_GpdPage.item.node_io = NumberConversion.binary2hex (aStringBuilder.ToString ());

				string jsonCommandIo = JsonConvert.SerializeObject (Node_io_GpdPage.item, Formatting.Indented);
				await App.Database.Update_DBAllNode_Item (Node_io_GpdPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);
				
			
		}

		public async void switch03_OnChange(object sender, ToggledEventArgs e)
		{

				Node_io_GpdPage.item.node_command = "command_io";
				Node_io_GpdPage.item.node_io_p = "0";
				var aStringBuilder = new StringBuilder (NumberConversion.hex2binary (Node_io_GpdPage.item.node_io));
				aStringBuilder.Remove (6, 1);
				aStringBuilder.Insert (6, Convert.ToByte (e.Value).ToString ());
				Node_io_GpdPage.item.node_io = NumberConversion.binary2hex (aStringBuilder.ToString ());

				string jsonCommandIo = JsonConvert.SerializeObject (Node_io_GpdPage.item, Formatting.Indented);
				await App.Database.Update_DBAllNode_Item (Node_io_GpdPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);

			
		}

		public async void switch04_OnChange(object sender, ToggledEventArgs e)
		{
				Node_io_GpdPage.item.node_command = "command_io";
				Node_io_GpdPage.item.node_io_p = "0";
				var aStringBuilder = new StringBuilder (NumberConversion.hex2binary (Node_io_GpdPage.item.node_io));
				aStringBuilder.Remove (7, 1);
				aStringBuilder.Insert (7, Convert.ToByte (e.Value).ToString ());
				Node_io_GpdPage.item.node_io = NumberConversion.binary2hex (aStringBuilder.ToString ());

				string jsonCommandIo = JsonConvert.SerializeObject (Node_io_GpdPage.item, Formatting.Indented);
				await App.Database.Update_DBAllNode_Item (Node_io_GpdPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);


		}**/

		public void testClick(object sender, EventArgs e)
		{
			//WebsocketManager.websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"FF\",\"node_command\":\"io_change_detected\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FE\",\"node_command\":\"io_change_detected\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"io_change_detected\"}]}");
			//WebsocketManager.websocketMaster.Send ("{\"cmd_alert\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FF\",\"node_command\":\"io_Alert\"}]}");
			WebsocketManager.websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"FF\",\"node_command\":\"io_change_detected\"}]}");
		}

	}
}

