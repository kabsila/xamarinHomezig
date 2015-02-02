using System;
using WebSocket4Net;
using Android.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;

namespace HomeZig.Android
{
	public class WebsocketManager
	{
		public static WebSocket websocketMaster;
		public WebsocketManager(string wsUrl)
		{
			Log.Info ("WebsocketManager","Connecting");
			websocketMaster = new WebSocket(wsUrl);
			websocketMaster.Opened += new EventHandler(websocket_Opened);
			websocketMaster.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
			websocketMaster.Closed += new EventHandler(websocket_Closed);
			websocketMaster.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
		}


		public void websocket_Opened(object sender, EventArgs e)
		{		
			//phoneNumberText.Text = "Opened";
			//websocket.Send("db_allnode");
			//websocket.Send("{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}");
			websocketMaster.Send ("{\"cmd_db_allnode\": [{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:ae]!\", \"node_status\": \"1\"},{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:kk]!\", \"node_status\": \"1\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:b2:16:5a]!\", \"node_status\": \"0\"}, {\"node_type\": \"0x0\", \"node_addr\": \"[00:13:a2:00:40:ad:57:e3]!\", \"node_status\": \"0\"}]}");
			//websocketMaster.Send ("{\"cmd_db_allnode\":[{\"Outlet\":[{\"node_addr\":\"123\", \"node_status\":\"1\"},{\"node_addr\":\"456\", \"node_status\":\"0\"}],\n\"Camera\":[{\"node_addr\":\"789\", \"node_status\":\"0\"},{\"node_addr\":\"121\", \"node_status\":\"1\"}]}]}");
			Log.Info ("websocket_Opened","WS Connected");
			new DeviceItemDatabase ();

		}

		public void websocket_Error(object sender, EventArgs e)
		{
			string tag = "Error";
			Log.Info (tag,e.ToString());
		}

		public void websocket_Closed(object sender, EventArgs e)
		{
			Log.Info("websocket_Closed", "WebsocketClosed");
		}

		public async void websocket_MessageReceived(object sender, MessageReceivedEventArgs  e)
		{
			//Log.Info ("MessageReceived" , e.Message);

			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";

			try
			{
				RootObject cmd = JsonConvert.DeserializeObject<RootObject>(e.Message);
				//var htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, Db_allnode>>(e.Message); 
				//string[] getRoot = cmd.GetType().GetProperties().GetValue(0).ToString().Split(' ');
				//string RootElement = getRoot[1];
				//getRoot = null;
				//string name2 = cmd.cmd_db_allnode[0].node_addr;
				//Log.Info ("MessageReceived222222" , cmd.cmd_db_allnode[0].ToString());

				//RootElement cmd = JsonConvert.DeserializeObject<RootElement>(e.Message);			

				foreach(var data in cmd.cmd_db_allnode)
				{
					try
					{
						await App.Database.Save_DBAllNode_Item(data);
					}
					catch (Exception exx)
					{
						Log.Info ("Exception" , exx.ToString());
					}
				}

				foreach(var i in await App.Database.GetItems())
				{
					Log.Info ("From Database" , i.node_addr);
				}


				switch ("swipe")
				{
					case "cmd_db_allnode":
					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {
							App.Navigation.PushAsync(new DeviceListPage());
						});
					})).Start();
					//Log.Info ("MessageReceived" , typeof(CmdDbAllnode).GetProperties()[0].Name);
					break;

					case "io_command":
					Log.Info ("MessageReceived3" , "");
					break;

					case "swipe":
					Log.Info ("MessageReceived3" , "swipe");

					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {
							//TabbedPage tw = new TabbedPage();
							//tw.Children.Add (new AllDeviveLoad(cmd.cmd_db_allnode){Title = "test1"});
							//tw.Children.Add (new Outlet2(){Title = "test2"});
							App.Navigation.PushAsync(new MenuTabPage());
						});
					})).Start();

					break;

					default:
					Log.Info ("MessageReceived" , e.Message);
					break;

				}

				//Example m = JsonConvert.DeserializeObject<Example>(e.Message);
				//string name2 = m.employees[0].firstName;
				//Log.Info (tag , name2);
			}
			catch (Exception ex)
			{
				Log.Info ("ExceptionMessageReceived" , ex.Message);
			}


		}
	}
}




