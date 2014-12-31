using System;
using WebSocket4Net;

namespace HomeZig.Android
{
	interface IWebsocket
	{
		void IWebsocket (string uri);
		void websocket_Opened(object sender, EventArgs e);
		void websocket_Error(object sender, EventArgs e);
		void websocket_Closed(object sender, EventArgs e);
		void websocket_MessageReceived(object sender, MessageReceivedEventArgs  e);
	}
}

