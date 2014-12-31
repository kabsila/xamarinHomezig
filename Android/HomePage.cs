using System;
using Xamarin.Forms;
using Android.Util;
using WebSocket4Net;
using Newtonsoft.Json;

/**
namespace HomeZig.Android
{
	public class HomePage : ContentPage, IWebsocket
	{


		public Button button1 = new Button
		{
			Text = " Go to Label Demo Page ",
			Font = Font.SystemFontOfSize(NamedSize.Large),
			BorderWidth = 1
		};

		public static WebSocket websocket;
		private Label header2;

		public HomePage()
		{

			Label header = new Label
			{
				Text = "Entry",
				Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.Center
			};

			Button button1 = new Button
			{
				Text = " Go to Label Demo Page ",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1
			};

			Button button2 = new Button
			{
				Text = " Button2 ",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1
			};

			Editor editor = new Editor
			{
				Text = "ws://echo.websocket.org",
				HorizontalOptions = LayoutOptions.Center
			};

			button1.Clicked += delegate {
				//await Navigation.PushAsync (new LabelDemoPage ());
				IWebsocket(editor.Text);
				websocket.Open();
				//header2.IsVisible = true;
				//await  
			};

			button2.Clicked += async (sender, args) =>
				await Navigation.PushAsync(new AllDevice());

			header2 = new Label
			{
				Text = "Header Test",
				IsVisible = false,
				Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.Center
			};

			// Build the page.
			this.Content = new StackLayout
			{
				Children = 
				{
					header,
					button1,
					editor,
					header2,
					new Entry
					{
						Keyboard = Keyboard.Email,
						Placeholder = "Enter email address",
						VerticalOptions = LayoutOptions.CenterAndExpand
					}

				}
			};
		}

		public void IWebsocket(string uri)
		{
			Log.Info ("CCCCC","AAAAAAAAAAAAAAAAAAAA");
			//websocket = new WebSocket("ws://echo.websocket.org");
			websocket = new WebSocket(uri);
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
			websocket.Send ("{\"cmd_db_allnode\": [{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:ae]!\", \"node_status\": \"1\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:b2:16:5a]!\", \"node_status\": \"1\"}, {\"node_type\": \"0x0\", \"node_addr\": \"[00:13:a2:00:40:ad:57:e3]!\", \"node_status\": \"1\"}]}");
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
			string tag = "myapp2";
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
			}

			new System.Threading.Thread(new System.Threading.ThreadStart(() => {
				Device.BeginInvokeOnMainThread (() => {
					header2.IsVisible = true; // this works!
				});
			})).Start();
		}

	}
}

**/