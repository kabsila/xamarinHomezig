using System;
using WebSocket4Net;
using Newtonsoft.Json;
using Android.Util;
using Xamarin.Forms;

namespace HomeZig.Android

{
	public class AllDevice : HomePage
	{



		public AllDevice ()
		{


			//Activity.RunOnUiThread(() => {
			//	button1.Text = "Method Complete";
			//});


		//	new System.Threading.Thread (new System.Threading.ThreadStart (() => {
				//Device.BeginInvokeOnMainThread (() => {

					
				//});
			//}));
			//await task;
			//button1.BindingContext = new {Name = "John Doe", Company = "Xamarin"};
			//Android.RunOnUiThread(() => button1.Text = "Updated from other thread");
			//button1.Text = "SSSSS";
			//button1.Clicked += delegate{Log.Info("ALLDEVICE", "CLICKED");};
			button1.Clicked += button1_Click;
			//Log.Info("ALLDEVICE2", button1.Text);

			button2.Clicked += button2_Click;

			//button1.Clicked += new EventHandler(delegate(object s, EventArgs args) {Log.Info("ALLDEVICE2", "CLICKED2");});

		}

		WebsocketManager wss =  new WebsocketManager();
		public void button1_Click(object sender, EventArgs e)
		{

			//WebsocketManager wss =  new WebsocketManager();
			//wss.websocket.Opened += new EventHandler(websocket_Opened);
			wss.websocket.Open ();

			//wss.websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived2);

			//App.Navigation.PushAsync (new Outlet());
			Log.Info("ALLDEVICE", "CLICKED");

		}

		public void button2_Click(object sender, EventArgs e)
		{
			wss.websocket.Send ("vvvvvvvvvvvvvvvvvvvvvvvv");
			//Page page = (Page)Activator.CreateInstance(typeof(Outlet));
			//this.Navigation.PushAsync(page);
			//App.Navigation.PushAsync (new Outlet());
			Log.Info("button2_Click", "kk");

		}

		public void IWebsocket(string uri)
		{
			//ws = new WebSocket(uri);
			//Log.Info ("CCCCC","AAAAAAAAAAAAAAAAAAAA");
			//ws = new WebSocket("ws://echo.websocket.org");
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

		public void websocket_MessageReceived2(object sender, MessageReceivedEventArgs e)
		{
			string tag = "myapp_AllDEVICE";
			Log.Info (tag , e.Message);
			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";

			try
			{
				//RootObject m = JsonConvert.DeserializeObject<RootObject>(e.Message);
				//string name2 = m.cmd_db_allnode[0].node_addr;
				//Log.Info (tag , name2);

				//Example m = JsonConvert.DeserializeObject<Example>(e.Message);
				//string name2 = m.employees[0].firstName;
				//Log.Info (tag , name2);
			}
			catch (Exception ex)
			{
				Log.Info (tag , ex.Message);
			}


		}

	}
}

