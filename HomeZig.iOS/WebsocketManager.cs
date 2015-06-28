using System;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Text;
//using Toasts.Forms.Plugin.Abstractions;
using System.Threading.Tasks;
using Connectivity.Plugin;
using MessageBar;


namespace HomeZig.iOS
{
	public class WebsocketManager : ContentPage
	{
		public static WebSocket websocketMaster;
		public static IPageManager ipm1;

		//string dataBasePath;
		public WebsocketManager()
		{
			
			//ipm1 = App.AppIpm;
			Console.WriteLine ("WebsocketManager Connecting");
			try{
				websocketMaster = new WebSocket(LoginPage.websocketUrl.Text);			
				websocketMaster.Opened += new EventHandler(websocket_Opened);
				websocketMaster.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
				websocketMaster.Closed += new EventHandler(websocket_Closed);
				websocketMaster.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
			}catch{			

				/**new System.Threading.Thread (new System.Threading.ThreadStart (() => {
					Device.BeginInvokeOnMainThread (async () => {
						var notificator = DependencyService.Get<IToastNotificator>();
						await notificator.Notify(ToastNotificationType.Error, 
							"Error", "Somethings Wrongs", TimeSpan.FromSeconds(3));
					});
				})).Start();**/
			}

			//var sqliteFilename = "HomezigSQLite.db3";
			//string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			//dataBasePath = Path.Combine(documentsPath, sqliteFilename);
		}

		public async void websocket_Opened(object sender, EventArgs e)
		{	

			Login_Page_Action.tmr.Stop ();

			Console.WriteLine ("websocket Websocket_Connected");

			new System.Threading.Thread (new System.Threading.ThreadStart (() => {
				Device.BeginInvokeOnMainThread (() => {
					//var notificator = DependencyService.Get<IToastNotificator>();
					//await global_notificator.Notify(ToastNotificationType.Success, 
						//"Connection complete", "Here we go !", TimeSpan.FromSeconds(2));
					MessageBarManager.SharedInstance.ShowMessage("Connect", "Connect Completeed", MessageType.Success);
					LoginPage.ConnectButton.IsEnabled = false;
					LoginPage.loginButton.IsEnabled = true;
					LoginPage.activityIndicator.IsRunning = false;
				});
			})).Start();


			foreach (var data in await App.Database.Get_flag_Login()) //check wa koiy login? 
			{
				Device.BeginInvokeOnMainThread (() => {
					LoginPage.ConnectButton.IsEnabled = false;
					LoginPage.loginButton.IsEnabled = false;
					LoginPage.activityIndicator.IsRunning = true;
				});

				LoginPage.loginButton.IsEnabled = false;
				//string flag = "";
				//flag = data.flagForLogin;
				if (data.flagForLogin.Equals ("pass")) {
					data.password = Login_Page_Action.sha256_hash (data.password);
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

		}

		public void websocket_Error(object sender, EventArgs e)
		{			
			websocketMaster.Close ();
			Device.BeginInvokeOnMainThread (() => {
				LoginPage.ConnectButton.IsEnabled = true;
				LoginPage.activityIndicator.IsRunning = false;
			});
			string tag = "Errorrrrrrrrrrrrrrrrrrrrrrrrrrr";

			Console.WriteLine (tag);

		}

		public void websocket_Closed(object sender, EventArgs e)
		{
			
			Console.WriteLine ("websocket_Closed");
			//websocketMaster.Dispose ();
			Device.BeginInvokeOnMainThread (() => {
				//await global_notificator.Notify(ToastNotificationType.Error, 
				//	"Error code: 101", "Websocket disconnected", TimeSpan.FromSeconds(2));
				MessageBarManager.SharedInstance.ShowMessage("Websocket Connection", "Websocket disconnected", MessageType.Error);
				LoginPage.activityIndicator.IsRunning = false;
				LoginPage.ConnectButton.IsEnabled = true;
				//AppDelegate.ipm.showLoginPage();
				App.current.showLoginPage();
				System.Diagnostics.Debug.WriteLine ("jsonCommandLogin {0}", "Go to login page");
			});
		}

		public async void websocket_MessageReceived(object sender, MessageReceivedEventArgs  e)
		{

			//Log.Info ("websocket_MessageReceived" , e.Message);
			Console.WriteLine ("websocket_MessageReceived \n {0}", e.Message);
			//string json = "{'employees': [{  'firstName':'John' , 'lastName':'Doe' },{  'firstName':'Anna' , 'lastName':'Smith' }, { 'firstName': 'Peter' ,  'lastName': 'Jones' }]}";
			//Log.Info ("MessageReceivedddddddddddd" , e.Message);
			try
			{
				RootObject cmd = JsonConvert.DeserializeObject<RootObject>(e.Message);
				//Db_allnode cmd = JsonConvert.DeserializeObject<Db_allnode>(e.Message);

				if(cmd.cmd_db_allnode != null){			
					
					Console.WriteLine("node_change_detected");
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
							Console.WriteLine("Exception {0}", exx.ToString());
						}

						if (isUpdate)
						{
							await App.Database.Update_DBAllNode_All_Item(data);
						}
					}
					 
					foreach(var data in await App.Database.GetItems())
					{
						#region io_value

						#endregion
						var ioNUmber = 0;
						if(data.node_deviceType.Equals(EnumtoString.EnumString(DeviceType.InWallSwitch))){
							ioNUmber = 2;
						}else if(data.node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
							ioNUmber = 4;
						}else{
							ioNUmber = 1;
						}

						try
						{
							//{"cmd_db_allnode":[{"node_io": "02", "node_type": "0x3ff01", "node_addr": "[00:13:a2:00:40:ad:bd:30]!", "node_status": "0", "node_command": "io_change_detected"}]}
							//for(var i = 0;i < ioNUmber;i++)
							//{
								//string io_state2 = find_io_value(i, data.node_io);
								//await App.Database.Update_NameByUser_ioValue2(data.node_io, io_state2, data.node_addr, i.ToString());
							//count++;
							//}

							for(var i = 0;i < ioNUmber;i++)
							{
								string io_state = find_io_value(i, data.node_io);
								await App.Database.Update_NameByUser_ioValue2(data.node_io, io_state, data.node_addr, i.ToString());
								if(!data.node_deviceType.Equals(EnumtoString.EnumString(DeviceType.UnknowDeviceType))){
									await App.Database.Save_NameByUser(data, i.ToString(), i.ToString(), io_state);
								}

							}
						}
						catch (Exception exx)
						{
							
							Console.WriteLine("Exception2 {0}", exx.ToString());
						}
					}
					foreach(var i in await App.Database.GetItems())
					{
						//Log.Info ("From Database" , i.node_addr);
						//Log.Info ("From node_status" , i.node_status);
					}

					foreach(var i in await App.Database.Get_NameByUser())
					{
						//System.Diagnostics.Debug.WriteLine("=====> {0}, {1}, {2}", i.node_addr, i.io_name_by_user, i.target_io);
						//Log.Info ("From Get_NameByUser" , String.Format("=====> {0}, ->{1}<-, {2}, {3}, {4}, {5}", i.node_addr, i.node_name_by_user, i.node_io, i.io_name_by_user, i.node_io_p, i.io_value));
						Console.WriteLine("=====> {0}, ->{1}<-, {2}, {3}, {4}, {5}", i.node_addr, i.node_name_by_user, i.node_io, i.io_name_by_user, i.node_io_p, i.io_value);
						//Log.Info ("From Get_NameByUser" , i.node_name_by_user);
						//Log.Info ("From Get_NameByUser" , i.io_name_by_user);
						//Log.Info ("From Get_NameByUser" , i.target_io);
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

					case "io_change_detected":
						//MessagingCenter.Send<ContentPage> (new ContentPage(), "ChangeSwitchDetect");
						Node_io_ItemPage.doSwitch = false;
						//must use message center
						if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.InWallSwitch))){
							
							Console.WriteLine("io_change_detected  InWallSwitch_ChangeSwitchDetect");
							new System.Threading.Thread (new System.Threading.ThreadStart (() => {
								Device.BeginInvokeOnMainThread ( async () => {									
									Node_io_ItemPage.doSwitch = false;
									Node_io_ItemPage.ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr(cmd.cmd_db_allnode[0].node_addr);										
								});
							})).Start();
							//MessagingCenter.Send<Node_io_ItemPage, string> (new Node_io_ItemPage(), "Node_io_Item_Change_Detected", cmd.cmd_db_allnode[0].node_addr);
						}else if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
							
							Console.WriteLine("io_change_detected  GeneralPurposeDetector_ChangeSwitchDetect");
							new System.Threading.Thread (new System.Threading.ThreadStart (() => {
								Device.BeginInvokeOnMainThread ( async () => {									
									System.Diagnostics.Debug.WriteLine ("Gpd_Change_Detectedvvvvvvvvvvvvvvvvvv");
									Node_io_GpdPage.ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr(cmd.cmd_db_allnode[0].node_addr);										
								});
							})).Start();

						}else{

						}

						Console.WriteLine("io_change_detected  ChangeSwitchDetect");
						break;

					case "db_allnode":
						
						Console.WriteLine("MessageReceived4  db_allnode");
						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								//AppDelegate.ipm.showMenuTabPage(AppDelegate.ipm);
							});
						})).Start();
					

						break;
						/**new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								ipm1.showMenuTabPage(ipm1);
							});
						})).Start();
						break;**/

					case "prevent_other_change_page":
						
						Console.WriteLine("prevent_other_change_page2");


						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								App.current.showMenuTabPage();
							});
						})).Start();
						break;

					case "listview_request":						
						IEnumerable<Db_allnode> dataSource = new List<Db_allnode>();
						if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){							
							Console.WriteLine("listview_request  GeneralPurposeDetector");
							dataSource = await App.Database.GetItemByDeviceType(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector));

						}else if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.InWallSwitch))){							
							Console.WriteLine("listview_request  InWallSwitch");
							dataSource = await App.Database.GetItemByDeviceType(EnumtoString.EnumString(DeviceType.InWallSwitch));
						}

							foreach (var data in dataSource) 
							{						

								if (data.node_status.Equals ("1")) { // 1 is OFFLINE , 0 is ONLINE
									var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");
									if (indexForRemove == -1) {
										data.name_by_user = data.name_by_user + "(OffLine)";
									} else {
										data.name_by_user = data.name_by_user.Remove (indexForRemove);
										data.name_by_user = data.name_by_user + "(OffLine)";
									}
									await App.Database.Update_DBAllNode_Item (data);

								} else {
									var indexForRemove = data.name_by_user.IndexOf ("(OffLine)");						
									if (indexForRemove != -1) {
										data.name_by_user = data.name_by_user.Remove (indexForRemove);
										await App.Database.Update_DBAllNode_Item (data);
									}
								}
							}

							//var ItemsSource = await App.Database.GetItemByDeviceType(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector));
							new System.Threading.Thread (new System.Threading.ThreadStart (() => {								
								Device.BeginInvokeOnMainThread (() => {									
									DeviceAddressListPage.addressListView.ItemsSource =  dataSource;//await App.Database.GetItemByDeviceType(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector));
									DeviceAddressListPage.addressListView.EndRefresh();
									DeviceAddressListPage.addressListView.IsRefreshing = false;
								});
							})).Start();
						//}
						break;	
					
						/**case "login_complete":
						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								ipm1.showMenuTabPage(ipm1);
							});
						})).Start();
						break;**/

					default:						
						Console.WriteLine("MessageReceived_default  {0}", e.Message);
						break;
					}

				}/**else if(cmd.node_change_detected != null){
					if(cmd.cmd_db_allnode[0].node_command.Equals("change_detected")){
						Node_io_ItemPage.doSwitch = false;
						//must use message center
						if(cmd.node_change_detected[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.InWallSwitch))){
							MessagingCenter.Send<Node_io_ItemPage, string> (new Node_io_ItemPage(), "Node_io_Item_Change_Detected", cmd.node_change_detected[0].node_addr);
						}else if(cmd.node_change_detected[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
							Log.Info ("io_change_detected" , "GeneralPurposeDetector_ChangeSwitchDetect");
							//Device.BeginInvokeOnMainThread (async () => {
							//	Node_io_GpdPage.ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr(cmd.node_change_detected[0].node_addr);
							//});
							MessagingCenter.Send<Node_io_GpdPage, string> (new Node_io_GpdPage(), "Node_io_Gpd_Change_Detected", cmd.node_change_detected[0].node_addr);
							//MessagingCenter.Send<ContentPage> (new ContentPage(), "Node_io_Gpd_Change_Detected");
						}else{

						}
						Log.Info ("io_change_detected" , "ChangeSwitchDetect");
					}
					
				}**/
					else if(cmd.cmd_login != null){

					//Admin_Delete_User_Page.ListOfusernameForDelete.Clear();

					foreach(var data in cmd.cmd_login)
					{
						#region cmd_login
						Console.WriteLine("cmd_login");
						#endregion

						if(data.flagForLogin.Equals("pass") && data.username.Equals(LoginPage.username.Text)){
							websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ab]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"prevent_other_change_page\"}, {\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"F8\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"prevent_other_change_page\"}]}");
							////no websocketMaster.Send("{\"cmd_login\":[{\"flagForLogin\":\"pass\",\"lastConnectWebscoketUrl\":\"ws://echo.websocket.org\"}]})");

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
							//websocketMaster.Send (FirstSend);
							#endregion
						}else if (data.flagForLogin.Equals("not_pass")){
							//DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
							Device.BeginInvokeOnMainThread (async () => {
								LoginPage.loginButton.IsEnabled = true;
								LoginPage.logoutButton.IsEnabled = true;
								LoginPage.activityIndicator.IsRunning = false;
								LoginPage.loginFail.IsVisible = true;
								MessageBarManager.SharedInstance.ShowMessage("Waring", "Username or Password is Invalid", MessageType.Error);
							});

						}else if (data.flagForLogin.Equals("add_user_success")){
							await App.Database.Add_Login_Username_Show_For_Del(data.username);

							Device.BeginInvokeOnMainThread (async () => {
								//Admin_Add_User_Page.username.BackgroundColor = Color.Green;
								//Admin_Add_User_Page.password.BackgroundColor = Color.Green;
								MessageBarManager.SharedInstance.ShowMessage("Account", data.username + " Added" , MessageType.Success);
							});
						}else if (data.flagForLogin.Equals("user_exits")){
							Device.BeginInvokeOnMainThread (async () => {
								//Admin_Add_User_Page.username.BackgroundColor = Color.Red;
								//Admin_Add_User_Page.password.BackgroundColor = Color.Red;
								MessageBarManager.SharedInstance.ShowMessage("Account", data.username + " Already in use" , MessageType.Error);
							});

						}else if (data.flagForLogin.Equals("user_password_change")){
							foreach (var LoginData in await App.Database.Get_flag_Login()) //check wa koiy login? 
							{
								await App.Database.Delete_Login_Item ();
								await App.Database.Save_Login_Item(LoginData.username, Change_Password_Page.newPassword.Text, LoginData.flagForLogin, LoginData.lastConnectWebscoketUrl);
								break;
							}
							Device.BeginInvokeOnMainThread (async () => {
								//Change_Password_Page.newPassword.BackgroundColor = Color.Green;
								MessageBarManager.SharedInstance.ShowMessage("Password", "\"New Password is Changed" , MessageType.Success);
							});
						}else if (data.flagForLogin.Equals("query_user")){

							await App.Database.Add_Login_Username_Show_For_Del(data.username);

						}else if (data.flagForLogin.Equals("user_deleted")){

							await App.Database.Delete_Login_Username_Show_For_Del(data.username);
							Device.BeginInvokeOnMainThread (async () => {
								Admin_Delete_User_Page.usernameForDelete.ItemsSource = await App.Database.Get_Login_Username_Show_For_Del();
								MessageBarManager.SharedInstance.ShowMessage("Account", data.username + " is Deleted" , MessageType.Success);
							});	
						}
					}


				}else if(cmd.cmd_remote != null){

					foreach(var data in cmd.cmd_remote)
					{
						//Log.Info ("cmd_remote" , "cmd_remote");

						foreach (var checkUser in await App.Database.Get_flag_Login())  
						{
							if(checkUser.username == data.remote_username){
								if(data.node_command.Equals("remote_code_success")){
									await App.Database.Save_RemoteData_Item(data.node_addr, data.remote_button_name);
									Device.BeginInvokeOnMainThread (async () => {
										//var notificator = DependencyService.Get<IToastNotificator>();
										//await notificator.Notify(ToastNotificationType.Success, 
										//	"Success", " Remote code saved", TimeSpan.FromSeconds(2));
									});
									/**Device.BeginInvokeOnMainThread (() => {
										Add_Remote_Single_Page.plsWaitText.TextColor = Color.Green;
										Add_Remote_Single_Page.plsWaitText.Text = "Remote code saved";
										Add_Remote_Single_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Single_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Double_Page.plsWaitText.TextColor = Color.Green;
										Add_Remote_Double_Page.plsWaitText.Text = "Remote code saved";
										Add_Remote_Double_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Double_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Triple_Page.plsWaitText.TextColor = Color.Green;
										Add_Remote_Triple_Page.plsWaitText.Text = "Remote code saved";
										Add_Remote_Triple_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = true;
									});**/

								}else if(data.node_command.Equals("remote_code_sync_database")){

									await App.Database.Save_RemoteData_Item(data.node_addr, data.remote_button_name);

								}else if(data.node_command.Equals("remote_code_fail")){
									Device.BeginInvokeOnMainThread (async () => {
										//var notificator = DependencyService.Get<IToastNotificator>();
										//await notificator.Notify(ToastNotificationType.Error, 
										//	"Warning", "Try Again", TimeSpan.FromSeconds(2));
									});
									/**Device.BeginInvokeOnMainThread (() => {
										//Add_Remote_Page.addRemotePageLayout.Children.Remove (Add_Remote_Page.plsWaitText);
										Add_Remote_Single_Page.plsWaitText.TextColor = Color.Red;
										Add_Remote_Single_Page.plsWaitText.Text = "Try Again";
										Add_Remote_Single_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Single_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Double_Page.plsWaitText.TextColor = Color.Red;
										Add_Remote_Double_Page.plsWaitText.Text = "Try Again";
										Add_Remote_Double_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Double_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Triple_Page.plsWaitText.TextColor = Color.Red;
										Add_Remote_Triple_Page.plsWaitText.Text = "Try Again";
										Add_Remote_Triple_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = true;
									});**/
								}else if(data.node_command.Equals("name_exist")){
									Device.BeginInvokeOnMainThread (async () => {
										//var notificator = DependencyService.Get<IToastNotificator>();
										//await notificator.Notify(ToastNotificationType.Warning, 
										//	"Warning", "This name is already in use", TimeSpan.FromSeconds(2));
									});
									/**	Device.BeginInvokeOnMainThread (() => {
										//Add_Remote_Page.addRemotePageLayout.Children.Remove (Add_Remote_Page.plsWaitText);
										Add_Remote_Single_Page.plsWaitText.TextColor = Color.Olive;
										Add_Remote_Single_Page.plsWaitText.Text = "This name is already in use";
										Add_Remote_Single_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Single_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Double_Page.plsWaitText.TextColor = Color.Olive;
										Add_Remote_Double_Page.plsWaitText.Text = "This name is already in use";
										Add_Remote_Double_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Double_Page.addRemoteSubmitButton.IsEnabled = true;
										Add_Remote_Triple_Page.plsWaitText.TextColor = Color.Olive;
										Add_Remote_Triple_Page.plsWaitText.Text = "This name is already in use";
										Add_Remote_Triple_Page.AddRemoteIndicator.IsRunning = false;
										Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = true;
									});**/
								}else if(data.node_command.Equals("remote_code_continue")){
									if(data.remote_code.Equals("1")){
										Device.BeginInvokeOnMainThread (async () => {
											//var notificator = DependencyService.Get<IToastNotificator>();
											//await notificator.Notify(ToastNotificationType.Success, 
											//	"Next button", "button 1 saved", TimeSpan.FromSeconds(2));
										});
										/**Device.BeginInvokeOnMainThread (() => {
											Add_Remote_Double_Page.addRemoteSubmitButton.IsEnabled = false;
											Add_Remote_Double_Page.plsWaitText.TextColor = Color.Olive;
											Add_Remote_Double_Page.plsWaitText.Text = "Next button 2";
											Add_Remote_Double_Page.AddRemoteIndicator.IsRunning = false;
											Add_Remote_Double_Page.addRemoteSubmitButton.IsEnabled = true;
											Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = false;
											Add_Remote_Triple_Page.plsWaitText.TextColor = Color.Olive;
											Add_Remote_Triple_Page.plsWaitText.Text = "Next button 2";
											Add_Remote_Triple_Page.AddRemoteIndicator.IsRunning = false;
											Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = true;
										});**/
									}else{
										Device.BeginInvokeOnMainThread (async () => {
											//var notificator = DependencyService.Get<IToastNotificator>();
											//await notificator.Notify(ToastNotificationType.Success, 
											//	"Next button", "button 2 saved", TimeSpan.FromSeconds(2));
										});
										/**Device.BeginInvokeOnMainThread (() => {
											Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = false;
											Add_Remote_Triple_Page.plsWaitText.TextColor = Color.Olive;
											Add_Remote_Triple_Page.plsWaitText.Text = "Next button 3";
											Add_Remote_Triple_Page.AddRemoteIndicator.IsRunning = false;
											Add_Remote_Triple_Page.addRemoteSubmitButton.IsEnabled = true;
										});**/
									}
								}else if(data.node_command.Equals("delete_remote_success")){
									await App.Database.Delete_RemoteData_Custom_Item(data.remote_button_name);
									Device.BeginInvokeOnMainThread (async () => {
										//var notificator = DependencyService.Get<IToastNotificator>();
										//await notificator.Notify(ToastNotificationType.Success, 
										//	"Success", "Item Deleted", TimeSpan.FromSeconds(2));
									});
									/**Device.BeginInvokeOnMainThread (async () => {
										Delete_Remote_Page.deleteStatus.TextColor = Color.Green;
										Delete_Remote_Page.deleteStatus.Text = "Item Deleted";
										Delete_Remote_Page.remoteButtonListName.ItemsSource = await App.Database.Get_RemoteData_Item();
									});**/

								}
							}
							break;
						}


					}
				}


				// This is where we copy in the prepopulated database
				//Console.WriteLine (path);
				/**if(await App.Database.GetItems())
				{
					foreach(var data in cmd.node_change_detected)
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
					foreach(var data in cmd.node_change_detected)
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
				//string name2 = cmd.node_change_detected[0].node_addr;
				//Log.Info ("MessageReceived222222" , cmd.node_change_detected[0].ToString());
				//RootElement cmd = JsonConvert.DeserializeObject<RootElement>(e.Message);	




				/**	foreach(var i in await App.Database.GetItemsNotDone())
				{
					Log.Info ("From Database" , i.node_deviceType);
				}**/



				//switch ("")
				/**switch (cmd.node_change_detected[0].node_command)
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
							//tw.Children.Add (new AllDeviveLoad(cmd.node_change_detected){Title = "test1"});
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
				Console.WriteLine ("ExceptionMessageReceived  {0}", ex.Message);
			}


		}

		string find_io_value(int position, string node_io)
		{
			string state = NumberConversion.hex2binary (node_io);
			//System.Diagnostics.Debug.WriteLine("testtttttt + " + state);
			string io_state = string.Empty;
			if(position.ToString().Equals("0")){
				io_state = state.Substring(7, 1);
				//System.Diagnostics.Debug.WriteLine("io_state 7,1 " + io_state);
				if (io_state.Equals ("0")) 
				{ 
					//System.Diagnostics.Debug.WriteLine("ecccc1111");
					io_state = "true";
				} 
				else 
				{
					io_state = "false";
				}
			}else if(position.ToString().Equals("1")){
				io_state = state.Substring(6, 1);
				//System.Diagnostics.Debug.WriteLine("io_state 6,1 " + io_state);
				if (io_state.Equals ("0")) 
				{
					//System.Diagnostics.Debug.WriteLine("ecccc2222");
					io_state = "true";
				} 
				else 
				{
					io_state = "false";
				}
			}else if(position.ToString().Equals("2")){
				io_state = state.Substring(5, 1);
				//System.Diagnostics.Debug.WriteLine("io_state 5,1 " + io_state);
				if (io_state.Equals ("0")) 
				{ 
					io_state = "true";
				} 
				else 
				{
					io_state = "false";
				}
			}else if(position.ToString().Equals("3")){
				io_state = state.Substring(4, 1);
				//System.Diagnostics.Debug.WriteLine("io_state 4,1 " + io_state);
				if (io_state.Equals ("0")) 
				{ 
					io_state = "true";
				} 
				else 
				{
					io_state = "false";
				}
			}

			return io_state;
		}

	}
}