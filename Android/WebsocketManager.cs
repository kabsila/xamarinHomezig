using System;
using WebSocket4Net;
using Android.Util;
using Newtonsoft.Json;


namespace HomeZig.Android
{
	public class WebsocketManager
	{
		public WebSocket websocket;
		public WebsocketManager()
		{
			//websocket = new WebSocket(uri);
			Log.Info ("CCCCC","AAAAAAAAAAAAAAAAAAAA");
			websocket = new WebSocket("ws://echo.websocket.org");
			websocket.Opened += new EventHandler(websocket_Opened);
			websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
			websocket.Closed += new EventHandler(websocket_Closed);
			websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);


		}


		public void websocket_Opened(object sender, EventArgs e)
		{
			string tag = "myapp";
			//phoneNumberText.Text = "Opened";
			//websocket.Send("db_allnode");
			//websocket.Send("{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}");
			//websocket.Send ("{\"cmd_db_allnode\": [{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:ae]!\", \"node_status\": \"1\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:b2:16:5a]!\", \"node_status\": \"1\"}, {\"node_type\": \"0x0\", \"node_addr\": \"[00:13:a2:00:40:ad:57:e3]!\", \"node_status\": \"1\"}]}");
			Log.Info (tag,"AAAAAAAAAAAAAAAAAAAA");

		}

		public void websocket_Error(object sender, EventArgs e)
		{
			string tag = "Error";
			Log.Info (tag,e.ToString());
		}

		public void websocket_Closed(object sender, EventArgs e)
		{
			//phoneNumberText.Text = "Closed";
		}

		public void websocket_MessageReceived(object sender, MessageReceivedEventArgs  e)
		{
			websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(new AllDevice().websocket_MessageReceived2);
			/**string tag = "myapp2";
			Log.Info ("MessageReceived" , e.Message);
			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";

			try
			{
				RootObject m = JsonConvert.DeserializeObject<RootObject>(e.Message);
				string name2 = m.cmd_db_allnode[0].node_addr;
				Log.Info (tag , name2);

				//Example m = JsonConvert.DeserializeObject<Example>(e.Message);
				//string name2 = m.employees[0].firstName;
				//Log.Info (tag , name2);
			}
			catch (Exception ex)
			{
				Log.Info (tag , ex.Message);
			}**/


		}
	}
}




