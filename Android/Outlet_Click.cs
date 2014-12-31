using System;
using Android.Util;
using WebSocket4Net;

namespace HomeZig.Android
{
	public class Outlet_Click : Outlet
	{
		public Outlet_Click ()
		{
			connect.Clicked += Connect_Click;
		}

		public void Connect_Click(object sender, EventArgs e)
		{
			//ws.Send ("{\"cmd_db_allnode\": [{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:ae]!\", \"node_status\": \"1\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:b2:16:5a]!\", \"node_status\": \"1\"}, {\"node_type\": \"0x0\", \"node_addr\": \"[00:13:a2:00:40:ad:57:e3]!\", \"node_status\": \"1\"}]}");
			Log.Info ("ws","send");
		}

		public void IWebsocket(string uri)
		{
			//ws = new WebSocket(uri);
			Log.Info ("CCCCC","AAAAAAAAAAAAAAAAAAAA");
			//ws = new WebSocket("ws://echo.websocket.org");
		//	ws = new WebSocket(uri);
			//ws.Opened += new EventHandler(websocket_Opened);
			//ws.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
			//ws.Closed += new EventHandler(websocket_Closed);
			//ws.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
			//ws.Open ();
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

		public void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			string tag = "myapp2";
			Log.Info ("MessageReceived" , e.Message);
			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";

			try
			{

			}
			catch (Exception ex)
			{
				Log.Info (tag , ex.Message);
			}


		}
	}
}

