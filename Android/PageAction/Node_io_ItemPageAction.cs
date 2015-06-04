using System;
using Xamarin.Forms;
using HomeZig.Android;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Text;

[assembly: Dependency (typeof (Node_io_ItemPageAction))]

namespace HomeZig.Android
{
	public class Node_io_ItemPageAction : I_Node_io_Item //IDeviceCall
	{
		public Node_io_ItemPageAction ()
		{

		}

		public async void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			
			if (Node_io_ItemPage.doSwitch) {
				var b = (Switch)sender;
				var NameByUserData = (NameByUser)b.BindingContext;
				//Node_io_ItemPage.NBitem = (NameByUser)b.BindingContext;

				var aStringBuilder = new StringBuilder (NumberConversion.hex2binary (Node_io_ItemPage.NBitem.node_io));
				if (NameByUserData.target_io.Equals ("1")) {				
					aStringBuilder.Remove (7, 1);
					aStringBuilder.Insert (7, Convert.ToByte (e.Value).ToString ());
				} else if (NameByUserData.target_io.Equals ("2")) {
					aStringBuilder.Remove (6, 1);
					aStringBuilder.Insert (6, Convert.ToByte (e.Value).ToString ());
				}

				Node_io_ItemPage.NBitem.node_io = NumberConversion.binary2hex (aStringBuilder.ToString ());
				NameByUserData.node_io = Node_io_ItemPage.NBitem.node_io;
				string jsonCommandIo = JsonConvert.SerializeObject (NameByUserData, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine ("in switcher_Toggled method json2", jsonCommandIo);

				await App.Database.Update_NameByUser_ioValue(NameByUserData.node_io, NameByUserData.node_addr);
				//WebsocketManager.websocketMaster.Send (jsonCommandIo);
			}

			//Node_io_ItemPage.doSwitch = true;
			//var aa = e.Value;
			//var pos = (((ListView) sender).GetValue(Label.TextProperty));
			//((ListView)sender).SelectedItem = co;
		}

		/**public async void switchLeft_OnChange(object sender, ToggledEventArgs e)
		{
			if(Node_io_ItemPage.doSwitch)
			{
				Node_io_ItemPage.item.node_command = "command_io";
				Node_io_ItemPage.item.node_io_p = "1";
				var aStringBuilder = new StringBuilder(NumberConversion.hex2binary (Node_io_ItemPage.item.node_io));
				aStringBuilder.Remove(6, 1);
				aStringBuilder.Insert(6, Convert.ToByte(e.Value).ToString());
				Node_io_ItemPage.item.node_io = NumberConversion.binary2hex(aStringBuilder.ToString());

				string jsonCommandIo = JsonConvert.SerializeObject(Node_io_ItemPage.item, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json222", jsonCommandIo);
				await App.Database.Update_DBAllNode_Item(Node_io_ItemPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);
				Node_io_ItemPage.doSwitch = true;
			}
			//WebsocketManager.websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff90\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"1\",\"node_io\":\"FC\",\"node_command\":\"command_io\"},{\"node_type\":\"0x3ff90\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"1\",\"node_io\":\"F8\",\"node_command\":\"command_io\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"F\",\"node_command\":\"command_io\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"command_io\"}]}");
		}

		public async void switchRight_OnChange(object sender, ToggledEventArgs e)
		{
			if (Node_io_ItemPage.doSwitch) 
			{
				Node_io_ItemPage.item.node_command = "command_io";
				Node_io_ItemPage.item.node_io_p = "0";
				var aStringBuilder = new StringBuilder (NumberConversion.hex2binary (Node_io_ItemPage.item.node_io));
				aStringBuilder.Remove (7, 1);
				aStringBuilder.Insert (7, Convert.ToByte (e.Value).ToString ());
				Node_io_ItemPage.item.node_io = NumberConversion.binary2hex (aStringBuilder.ToString ());

				string jsonCommandIo = JsonConvert.SerializeObject (Node_io_ItemPage.item, Formatting.Indented);
				System.Diagnostics.Debug.WriteLine("in switcher_Toggled method json222", jsonCommandIo);
				await App.Database.Update_DBAllNode_Item (Node_io_ItemPage.item);
				WebsocketManager.websocketMaster.Send (jsonCommandIo);
				Node_io_ItemPage.doSwitch = true;
			}
		}**/

		public void testClick(object sender, EventArgs e)
		{
			WebsocketManager.websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FF\",\"node_command\":\"io_change_detected\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"F8\",\"node_command\":\"io_change_detected\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"io_change_detected\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"io_change_detected\"}]}");
		}
	}
}

