using System;
using WebSocket4Net;
using Android.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Text;

namespace HomeZig.Android
{
	public class WebsocketManager
	{
		public static WebSocket websocketMaster;
		public static IPageManager ipm1;

		//string dataBasePath;
		public WebsocketManager(string wsUrl, IPageManager ipm2)
		{
			ipm1 = ipm2;
			Log.Info ("WebsocketManager","Connecting");
			websocketMaster = new WebSocket(wsUrl);
			websocketMaster.Opened += new EventHandler(websocket_Opened);
			websocketMaster.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
			websocketMaster.Closed += new EventHandler(websocket_Closed);
			websocketMaster.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);

			//var sqliteFilename = "HomezigSQLite.db3";
			//string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			//dataBasePath = Path.Combine(documentsPath, sqliteFilename);
		}


		public async void websocket_Opened(object sender, EventArgs e)
		{		
			//phoneNumberText.Text = "Opened";
			//websocket.Send("db_allnode");
			//websocket.Send("{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}");
			//websocketMaster.Send ("{\"cmd_db_allnode\": [{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:ae]!\", \"node_status\": \"1\"},{\"node_type\": \"0x3ff90\", \"node_addr\": \"[00:13:a2:00:40:ad:58:kk]!\", \"node_status\": \"1\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:b2:16:5a]!\", \"node_status\": \"0\"}, {\"node_type\": \"0xa001a\", \"node_addr\": \"[00:13:a2:00:40:ad:57:e3]!\", \"node_status\": \"0\"}]}");
			//websocketMaster.Send ("{\"cmd_db_allnode\":[{\"Outlet\":[{\"node_addr\":\"123\", \"node_status\":\"1\"},{\"node_addr\":\"456\", \"node_status\":\"0\"}],\n\"Camera\":[{\"node_addr\":\"789\", \"node_status\":\"0\"},{\"node_addr\":\"121\", \"node_status\":\"1\"}]}]}");
			//websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff90\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"1\",\"node_io\":\"FC\"},{\"node_type\":\"0x3ff90\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"1\",\"node_io\":\"F8\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FF\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\"}]}");

			Device.BeginInvokeOnMainThread (() => {
				//HomePage.ConnectButton.IsEnabled = false;
				//HomePage.activityIndicator.IsRunning = false;
				LoginPage.ConnectButton.IsEnabled = false;
				LoginPage.loginButton.IsEnabled = true;
				LoginPage.activityIndicator.IsRunning = false;
			});

			foreach (var data in await App.Database.Get_flag_Login()) //check wa koiy login? 
			{
				LoginPage.loginButton.IsEnabled = false;
				//string flag = "";
				//flag = data.flagForLogin;
				if (data.flagForLogin.Equals ("pass")) {
					data.password = LoginClick.sha256_hash (data.password);
					data.node_command = "check_login";
					string jsonCommandLogin = JsonConvert.SerializeObject(data, Formatting.Indented);
					//var jsonCommandLogin = new StringBuilder(JsonConvert.SerializeObject(data, Formatting.Indented));
					//jsonCommandLogin.Insert (0,"{\"cmd_login\":[");
					//jsonCommandLogin.Insert (jsonCommandLogin.Length, "]}");
					//Log.Info ("jsonCommandLogin" , jsonCommandLogin.ToString());
					WebsocketManager.websocketMaster.Send (jsonCommandLogin.ToString());

				} 
				break;
			}
			//websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"F8\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"db_allnode\"}]}");

			/**
			#region FirstSendToGateway
			Db_allnode db = new Db_allnode ();
			db.node_command = "db_allnode";
			db.ID = 0;
			db.nodeStatusToString = "";
			db.node_addr = "";
			db.node_deviceType = "";
			db.node_io = "";
			db.node_status = "";
			db.node_type = "";
			db.node_io_p = "";
			//db.name;
			var FirstSend = JsonConvert.SerializeObject(db);
			websocketMaster.Send (FirstSend);
			#endregion**/

		}

		public void websocket_Error(object sender, EventArgs e)
		{
			websocketMaster.Close ();
			Device.BeginInvokeOnMainThread (() => {
				HomePage.ConnectButton.IsEnabled = true;
				HomePage.activityIndicator.IsRunning = false;
			});
			string tag = "Errorrrrrrrrrrrrrrrrrrrrrrrrrrr";
			Log.Info (tag,e.ToString());
		}

		public void websocket_Closed(object sender, EventArgs e)
		{
			Log.Info("websocket_Closed", "WebsocketClosed");
			Device.BeginInvokeOnMainThread (() => {
				HomePage.ConnectButton.IsEnabled = true;
				HomePage.activityIndicator.IsRunning = false;
			});
		}

		public async void websocket_MessageReceived(object sender, MessageReceivedEventArgs  e)
		{

			Log.Info ("websocket_MessageReceived" , e.Message);

			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";
			//Log.Info ("MessageReceivedddddddddddd" , e.Message);
			try
			{
				RootObject cmd = JsonConvert.DeserializeObject<RootObject>(e.Message);
				//Db_allnode cmd = JsonConvert.DeserializeObject<Db_allnode>(e.Message);

				if(cmd.cmd_db_allnode != null){			
					Log.Info ("AAAAAAAAAAAAAAAAAAAA" , "AAA");
					foreach(var data in cmd.cmd_db_allnode)
					{
						bool isUpdate = false;
						try
						{
							await App.Database.Save_DBAllNode_Item(data);
						}
						catch (Exception exx)
						{
							isUpdate = true;
							Log.Info ("Exception" , exx.ToString());
						}

						if (isUpdate)
						{
							await App.Database.Update_DBAllNode_All_Item(data);
						}
					}

					foreach(var i in await App.Database.GetItems())
					{
						Log.Info ("From Database" , i.node_addr);
						Log.Info ("From node_status" , i.node_status);
					}

					switch (cmd.cmd_db_allnode[0].node_command)
					{
					case "db_allnode2":
						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								App.Navigation.PushAsync(new DeviceAddressListPage());
							});
						})).Start();
						//Log.Info ("MessageReceived" , typeof(CmdDbAllnode).GetProperties()[0].Name);
						break;

					case "command_io":
						MessagingCenter.Send<ContentPage> (new ContentPage(), "ChangeSwitchDetect");
						Log.Info ("MessageReceived3" , "ChangeSwitchDetect");
						break;

					case "db_allnode":
						Log.Info ("MessageReceived4" , "swipe");

						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								//TabbedPage tw = new TabbedPage();
								//tw.Children.Add (new AllDeviveLoad(cmd.cmd_db_allnode){Title = "test1"});
								//tw.Children.Add (new Outlet2(){Title = "test2"});
								//App.Navigation.PushAsync(new MenuTabPage());
								//App.Navigation.PushAsync(new NavigationPage(new MenuTabPage()));
								ipm1.showMenuTabPage();
							});
						})).Start();
						break;

					case "login_complete":
						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								ipm1.showMenuTabPage();
							});
						})).Start();
						break;



					default:
						Log.Info ("MessageReceived_default" , e.Message);
						break;

					}

				}else if(cmd.cmd_login != null){

					Admin_Delete_User_Page.ListOfusernameForDelete.Clear();

					foreach(var data in cmd.cmd_login)
					{
						Log.Info ("BBBBBBBBBBBBBBBBBBB" , "BBBBBB");
						if(data.flagForLogin.Equals("pass") && data.username.Equals(LoginPage.username.Text)){
							//websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"F8\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"db_allnode\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"db_allnode\"}]}");
							await App.Database.Save_Login_Item (LoginPage.username.Text, LoginPage.password.Text, data.flagForLogin, data.lastConnectWebscoketUrl);
							#region FirstSendToGateway
							Db_allnode db = new Db_allnode ();
							db.node_command = "db_allnode";
							db.ID = 0;
							db.nodeStatusToString = "";
							db.node_addr = "";
							db.node_deviceType = "";
							db.node_io = "";
							db.node_status = "";
							db.node_type = "";
							db.node_io_p = "";
							//db.name;
							var FirstSend = JsonConvert.SerializeObject(db);
							websocketMaster.Send (FirstSend);
							#endregion
						}else if (data.flagForLogin.Equals("not_pass")){
							//DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
							LoginPage.loginButton.IsEnabled = true;
							LoginPage.logoutButton.IsEnabled = true;
						}else if (data.flagForLogin.Equals("add_user_success")){
							Device.BeginInvokeOnMainThread (() => {
								Admin_Add_User_Page.username.BackgroundColor = Color.Green;
								Admin_Add_User_Page.password.BackgroundColor = Color.Green;
							});
						}else if (data.flagForLogin.Equals("user_exits")){
							Device.BeginInvokeOnMainThread (() => {
								Admin_Add_User_Page.username.BackgroundColor = Color.Red;
								Admin_Add_User_Page.password.BackgroundColor = Color.Red;
							});
						}else if (data.flagForLogin.Equals("user_password_change")){
							foreach (var LoginData in await App.Database.Get_flag_Login()) //check wa koiy login? 
							{
								await App.Database.Delete_Login_Item ();
								await App.Database.Save_Login_Item(LoginData.username, Change_Password_Page.newPassword.Text, LoginData.flagForLogin, LoginData.lastConnectWebscoketUrl);
								break;
							}
							Device.BeginInvokeOnMainThread (() => {
								Change_Password_Page.newPassword.BackgroundColor = Color.Green;
							});
						}else if (data.flagForLogin.Equals("query_user")){
							Admin_Delete_User_Page.ListOfusernameForDelete.Add(new Login(data.username));
							new System.Threading.Thread (new System.Threading.ThreadStart (() => {
								Device.BeginInvokeOnMainThread (() => {
									MessagingCenter.Send<ContentPage> (new ContentPage(), "user_for_delete");
								});
							})).Start();

						}
					}
				}
				Log.Info ("CCCCCCCCCCCCCCCCCC" , "CCCC");
				// This is where we copy in the prepopulated database
				//Console.WriteLine (path);
				/**if(await App.Database.GetItems())
				{
					foreach(var data in cmd.cmd_db_allnode)
					{
						try
						{
							await App.Database.Update_Node_Io(data.node_io,data.node_addr);
						}
						catch (Exception exx)
						{
							Log.Info ("Exception" , exx.ToString());
						}
					}

				}
				else
				{
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
				}**/



				//var htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, Db_allnode>>(e.Message); 
				//string[] getRoot = cmd.GetType().GetProperties().GetValue(0).ToString().Split(' ');
				//string RootElement = getRoot[1];
				//getRoot = null;
				//string name2 = cmd.cmd_db_allnode[0].node_addr;
				//Log.Info ("MessageReceived222222" , cmd.cmd_db_allnode[0].ToString());
				//RootElement cmd = JsonConvert.DeserializeObject<RootElement>(e.Message);	




			/**	foreach(var i in await App.Database.GetItemsNotDone())
				{
					Log.Info ("From Database" , i.node_deviceType);
				}**/



				//switch ("")
				/**switch (cmd.cmd_db_allnode[0].node_command)
				{
					case "db_allnode2":
					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {
							App.Navigation.PushAsync(new DeviceAddressListPage());
						});
					})).Start();
					//Log.Info ("MessageReceived" , typeof(CmdDbAllnode).GetProperties()[0].Name);
					break;

					case "command_io":
					MessagingCenter.Send<ContentPage> (new ContentPage(), "ChangeSwitchDetect");
					Log.Info ("MessageReceived3" , "ChangeSwitchDetect");
					break;

					case "db_allnode":
					Log.Info ("MessageReceived4" , "swipe");

					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {
							//TabbedPage tw = new TabbedPage();
							//tw.Children.Add (new AllDeviveLoad(cmd.cmd_db_allnode){Title = "test1"});
							//tw.Children.Add (new Outlet2(){Title = "test2"});
							//App.Navigation.PushAsync(new MenuTabPage());
							//App.Navigation.PushAsync(new NavigationPage(new MenuTabPage()));
							ipm1.showMenuTabPage();
						});
					})).Start();
					break;

					case "login_complete":
					new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread (() => {
							ipm1.showMenuTabPage();
						});
					})).Start();
					break;



					default:
					Log.Info ("MessageReceived_default" , e.Message);
					break;

				}**/

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




