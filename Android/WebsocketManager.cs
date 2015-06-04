using System;
using WebSocket4Net;
using Android.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Text;
using Toasts.Forms.Plugin.Abstractions;
using System.Threading.Tasks;

namespace HomeZig.Android
{
	public class WebsocketManager : ContentPage
	{
		public static WebSocket websocketMaster;
		public static IPageManager ipm1;
		IToastNotificator global_notificator = DependencyService.Get<IToastNotificator>();
		//string dataBasePath;
		public WebsocketManager()
		{
			
			ipm1 = MainActivity.ipm;
			Log.Info ("WebsocketManager","Connecting");
			try{
				websocketMaster = new WebSocket(LoginPage.websocketUrl.Text);			
				websocketMaster.Opened += new EventHandler(websocket_Opened);
				websocketMaster.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
				websocketMaster.Closed += new EventHandler(websocket_Closed);
				websocketMaster.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
			}catch{
				Device.BeginInvokeOnMainThread (async () => {
					var notificator = DependencyService.Get<IToastNotificator>();
					await notificator.Notify(ToastNotificationType.Error, 
						"Error", "Somethings Wrongs", TimeSpan.FromSeconds(3));
				});
			}
			//var sqliteFilename = "HomezigSQLite.db3";
			//string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			//dataBasePath = Path.Combine(documentsPath, sqliteFilename);
		}


		public async void websocket_Opened(object sender, EventArgs e)
		{	

			Login_Page_Action.tmr.Stop ();
			Log.Info("websocket", "Websocket_Connected");

			Device.BeginInvokeOnMainThread (async () => {
				//var notificator = DependencyService.Get<IToastNotificator>();
				await global_notificator.Notify(ToastNotificationType.Success, 
					"Connection complete", "Here we go !", TimeSpan.FromSeconds(5));
				
				LoginPage.ConnectButton.IsEnabled = false;
				LoginPage.loginButton.IsEnabled = true;
				LoginPage.activityIndicator.IsRunning = false;
			});

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
			Log.Info (tag,e.ToString());
		}

		public void websocket_Closed(object sender, EventArgs e)
		{
			Log.Info("websocket_Closed", "WebsocketClosed");
			//websocketMaster.Dispose ();
			Device.BeginInvokeOnMainThread (async () => {
				await global_notificator.Notify(ToastNotificationType.Error, 
					"Error code: 101", "Websocket disconnected", TimeSpan.FromSeconds(2));
				LoginPage.ConnectButton.IsEnabled = true;
				MainActivity.ipm.showLoginPage();

				System.Diagnostics.Debug.WriteLine ("jsonCommandLogin {0}", "Go to login page");
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
					Log.Info ("node_change_detected" , "node_change_detected");

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


					foreach(var data in await App.Database.GetItems())
					{
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
							await App.Database.Update_NameByUser_ioValue(data.node_io, data.node_addr);
							for(var i = 1;i <= ioNUmber;i++)
							{
								await App.Database.Save_NameByUser(data, i.ToString(), i.ToString());
							}
						}
						catch (Exception exx)
						{
							Log.Info ("Exception2" , exx.ToString());
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
						Log.Info ("From Get_NameByUser" , String.Format("=====> {0}, ->{1}<-, {2}, {3}, {4}, {5}", i.node_addr, i.node_name_by_user, i.node_io, i.io_name_by_user, i.target_io, i.io_value));
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
							MessagingCenter.Send<Node_io_ItemPage, string> (new Node_io_ItemPage(), "Node_io_Item_Change_Detected", cmd.cmd_db_allnode[0].node_addr);
						}else if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
							Log.Info ("io_change_detected" , "GeneralPurposeDetector_ChangeSwitchDetect");
							//Device.BeginInvokeOnMainThread (async () => {
							//	Node_io_GpdPage.ioListView.ItemsSource = await App.Database.Get_NameByUser_by_addr(cmd.node_change_detected[0].node_addr);
							//});
							MessagingCenter.Send<Node_io_GpdPage, string> (new Node_io_GpdPage(), "Node_io_Gpd_Change_Detected", cmd.cmd_db_allnode[0].node_addr);
							//MessagingCenter.Send<ContentPage> (new ContentPage(), "Node_io_Gpd_Change_Detected");
						}else{

						}



						Log.Info ("io_change_detected" , "ChangeSwitchDetect");
						break;

					case "db_allnode":
						Log.Info ("MessageReceived4" , "db_allnode");
						Device.BeginInvokeOnMainThread ( () => {

							MainActivity.ipm.showMenuTabPage(MainActivity.ipm);
							Log.Info ("prevent_other_change_page" ,"CCCCCCCCCCCCCCCCCCCC");

						});
						break;
						/**new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								ipm1.showMenuTabPage(ipm1);
							});
						})).Start();
						break;**/

					case "prevent_other_change_page":
						Log.Info ("prevent_other_change_page" ,"UUUUUUUUUUUUUUUUUUU");
						//new System.Threading.Thread (new System.Threading.ThreadStart (() => {
						Device.BeginInvokeOnMainThread ( () => {

							MainActivity.ipm.showMenuTabPage(MainActivity.ipm);
							Log.Info ("prevent_other_change_page" ,"CCCCCCCCCCCCCCCCCCCC");

						});
						//})).Start();
						break;

					case "listview_request":
						if(cmd.cmd_db_allnode[0].node_deviceType.Equals(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector))){
							Log.Info ("listview_request" ,"keepppppppppppppp");
							new System.Threading.Thread (new System.Threading.ThreadStart (() => {
								Device.BeginInvokeOnMainThread (async () => {
									DeviceAddressListPage.addressListView.EndRefresh();
									DeviceAddressListPage.addressListView.IsRefreshing = false;
									DeviceAddressListPage.addressListView.ItemsSource =  await App.Database.GetItemByDeviceType(EnumtoString.EnumString(DeviceType.GeneralPurposeDetector));

								});
							})).Start();

						}
						break;	
					
						/**case "login_complete":
						new System.Threading.Thread (new System.Threading.ThreadStart (() => {
							Device.BeginInvokeOnMainThread (() => {
								ipm1.showMenuTabPage(ipm1);
							});
						})).Start();
						break;**/



					default:
						Log.Info ("MessageReceived_default" , e.Message);
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
						Log.Info ("cmd_login" , "cmd_login");
						#endregion

						if(data.flagForLogin.Equals("pass") && data.username.Equals(LoginPage.username.Text)){
							websocketMaster.Send ("{\"cmd_db_allnode\":[{\"node_type\":\"0x3ff01\",\"node_addr\":\"[00:13:a2:00:40:ad:58:ae]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:ad:58:kk]!\",\"node_status\":\"0\",\"node_io\":\"F8\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0x3ff11\",\"node_addr\":\"[00:13:a2:00:40:b2:16:5a]!\",\"node_status\":\"0\",\"node_io\":\"FC\",\"node_command\":\"prevent_other_change_page\"},{\"node_type\":\"0xa001a\",\"node_addr\":\"[00:13:a2:00:40:ad:57:e3]!\",\"node_status\":\"0\",\"node_io\":\"FA\",\"node_command\":\"prevent_other_change_page\"}]}");
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

								var notificator = DependencyService.Get<IToastNotificator>();
								await notificator.Notify(ToastNotificationType.Warning, 
									"Waring", "Username or Password is Invalid", TimeSpan.FromSeconds(2));
							});

						}else if (data.flagForLogin.Equals("add_user_success")){
							await App.Database.Add_Login_Username_Show_For_Del(data.username);

							Device.BeginInvokeOnMainThread (async () => {
								//Admin_Add_User_Page.username.BackgroundColor = Color.Green;
								//Admin_Add_User_Page.password.BackgroundColor = Color.Green;
								var notificator = DependencyService.Get<IToastNotificator>();
								await notificator.Notify(ToastNotificationType.Success, 
									"Success", data.username + " Added", TimeSpan.FromSeconds(2));
							});
						}else if (data.flagForLogin.Equals("user_exits")){
							Device.BeginInvokeOnMainThread (async () => {
								//Admin_Add_User_Page.username.BackgroundColor = Color.Red;
								//Admin_Add_User_Page.password.BackgroundColor = Color.Red;
								var notificator = DependencyService.Get<IToastNotificator>();
								await notificator.Notify(ToastNotificationType.Warning, 
									"Warning", data.username + " Already in use", TimeSpan.FromSeconds(2));
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
								var notificator = DependencyService.Get<IToastNotificator>();
								await notificator.Notify(ToastNotificationType.Success, 
									"Success", "New Password is Changed", TimeSpan.FromSeconds(2));
							});
						}else if (data.flagForLogin.Equals("query_user")){

							await App.Database.Add_Login_Username_Show_For_Del(data.username);

						}else if (data.flagForLogin.Equals("user_deleted")){

							await App.Database.Delete_Login_Username_Show_For_Del(data.username);
							Device.BeginInvokeOnMainThread (async () => {
								Admin_Delete_User_Page.usernameForDelete.ItemsSource = await App.Database.Get_Login_Username_Show_For_Del();
								var notificator = DependencyService.Get<IToastNotificator>();
								await notificator.Notify(ToastNotificationType.Success, 
									"Success", data.username + " is Deleted", TimeSpan.FromSeconds(2));
							});	
						}
					}


				}else if(cmd.cmd_remote != null){

					foreach(var data in cmd.cmd_remote)
					{
						Log.Info ("cmd_remote" , "cmd_remote");

						foreach (var checkUser in await App.Database.Get_flag_Login())  
						{
							if(checkUser.username == data.remote_username){
								if(data.node_command.Equals("remote_code_success")){
									await App.Database.Save_RemoteData_Item(data.node_addr, data.remote_button_name);
									Device.BeginInvokeOnMainThread (async () => {
										var notificator = DependencyService.Get<IToastNotificator>();
										await notificator.Notify(ToastNotificationType.Success, 
											"Success", " Remote code saved", TimeSpan.FromSeconds(2));
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
										var notificator = DependencyService.Get<IToastNotificator>();
										await notificator.Notify(ToastNotificationType.Error, 
											"Warning", "Try Again", TimeSpan.FromSeconds(2));
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
										var notificator = DependencyService.Get<IToastNotificator>();
										await notificator.Notify(ToastNotificationType.Warning, 
											"Warning", "This name is already in use", TimeSpan.FromSeconds(2));
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
											var notificator = DependencyService.Get<IToastNotificator>();
											await notificator.Notify(ToastNotificationType.Success, 
												"Next button", "button 1 saved", TimeSpan.FromSeconds(2));
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
											var notificator = DependencyService.Get<IToastNotificator>();
											await notificator.Notify(ToastNotificationType.Success, 
												"Next button", "button 2 saved", TimeSpan.FromSeconds(2));
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
										var notificator = DependencyService.Get<IToastNotificator>();
										await notificator.Notify(ToastNotificationType.Success, 
											"Success", "Item Deleted", TimeSpan.FromSeconds(2));
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
				Log.Info ("ExceptionMessageReceived" , ex.Message);
			}


		}

	}
}