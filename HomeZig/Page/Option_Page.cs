using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HomeZig
{
	public class Option_Page : ContentPage
	{

		TableSection ts = new TableSection ();
		TextCell adduser = new TextCell 
		{
			Text = "Add new user",
			//Command = navigateCommand,
			//CommandParameter = typeof(Admin_Add_User_Page)
		};

		TextCell deluser = new TextCell 
		{
			Text = "Delete user",
			//Command = navigateCommand,
			//CommandParameter = typeof(Admin_Add_User_Page)
		};

		TextCell changePassword = new TextCell 
		{
			Text = "Change password",
			//Command = navigateCommand,
			//CommandParameter = typeof(Change_Password_Page)
		};

		TextCell logout = new TextCell {
			Text = "Log Out",
			//Command = logoutCommand,
			//CommandParameter = typeof(LoginPage),

		};

		public Option_Page (IPageManager ipm)
		{
			Command<Type> navigateCommand = new Command<Type>(async (Type pageType) =>
			{
				Page page = (Page)Activator.CreateInstance(pageType);
				await this.Navigation.PushAsync(page);
			});

			Command<Type> logoutCommand = new Command<Type>(async (Type pageType) =>
				{
					await App.Database.Delete_Login_Item();
					LoginPage.username.Text = "";
					LoginPage.password.Text = "";
					Device.BeginInvokeOnMainThread (() => {
						LoginPage.username.IsEnabled = true;
						LoginPage.password.IsEnabled = true;
						LoginPage.loginButton.IsEnabled = true;
						LoginPage.logoutButton.IsEnabled = false;
						LoginPage.activityIndicator.IsRunning = false;
						LoginPage.ConnectButton.IsEnabled = true;
					});

					await App.Database.Delete_RemoteData_Item();
					await App.Database.Delete_All_Login_Username_Show_For_Del ();
					ipm.showLoginPageDis();
					//Page page = (Page)Activator.CreateInstance(pageType);
					//await this.Navigation.PushAsync(page);
				});

			adduser.Command = navigateCommand;
			adduser.CommandParameter = typeof(Admin_Add_User_Page);

			deluser.Command = navigateCommand;
			deluser.CommandParameter = typeof(Admin_Delete_User_Page);

			changePassword.Command = navigateCommand;
			changePassword.CommandParameter = typeof(Change_Password_Page);

			logout.Command = logoutCommand;
			logout.CommandParameter = typeof(LoginPage);

			TableView tw = new TableView ();
			TableRoot tr = new TableRoot ();
			//TableSection ts = new TableSection ();
			ts.Title = "Account";

			Check_admin_for_add_menu ();
			//ts.Add (adduser);
			//ts.Add (changePassword);
			//ts.Add (logout);

			tr.Add (ts);		
			tw.Intent = TableIntent.Menu;
			tw.Root = tr;

			this.Title = "Option";
			this.Content = tw;
			/**
				this.Title = "Option";
				this.Content = new TableView {
				Intent = TableIntent.Menu,
				Root = new TableRoot {
					new TableSection ("Account") {
						adduser,

						new TextCell {
							Text = "Change password",
							Command = navigateCommand,
							CommandParameter = typeof(Change_Password_Page)
						},

						new TextCell {
							Text = "Log Out",
							Command = logoutCommand,
							CommandParameter = typeof(LoginPage),

						},
					}
				}
			};**/


		}

		async void Check_admin_for_add_menu()
		{
			foreach (var data in await App.Database.Get_flag_Login()) 
			{
				if (data.username.Equals ("admin")) {
					ts.Add (adduser);
					ts.Add (deluser);
					ts.Add (logout);
				} else {
					ts.Add (changePassword);
					ts.Add (logout);
				}
				break;
			}

		}
	}
}

