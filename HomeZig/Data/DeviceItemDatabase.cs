using System;
using SQLite.Net;
using SQLite.Net.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeZig
{
	public class DeviceItemDatabase 
	{
		//static object locker = new object ();

		SQLiteAsyncConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public DeviceItemDatabase()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			// create the tables
			database.CreateTableAsync<Login>();
			database.CreateTableAsync<Db_allnode>();
			database.CreateTableAsync<NameByUser>();
			database.CreateTableAsync<RemoteData>();
		}

		#region Login
		public async Task<IEnumerable<Login>> Save_Login_Item (string username, string password, string flagForLogin, string lastConnectWebscoketUrl)
		{
			return await database.QueryAsync<Login>(String.Format("INSERT INTO [Login] ([username], [password], [flagForLogin], [lastConnectWebscoketUrl]) VALUES ('{0}', '{1}', '{2}', '{3}')",username, password, flagForLogin, lastConnectWebscoketUrl));
		}

		public async Task<IEnumerable<Login>> Update_Login_Item (string username, string password, string flagForLogin)
		{
			return await database.QueryAsync<Login>(String.Format("UPDATE [Login] SET [username] = '{0}', [password] = '{1}', [flagForLogin] = '{2}' WHERE [ID] = 0",username, password, flagForLogin));
		}

		public async Task<IEnumerable<Login>> Delete_Login_Item ()
		{
			return await database.QueryAsync<Login>("DELETE FROM [Login]");
		}

		public async Task<IEnumerable<Login>> Get_flag_Login ()
		{
			return await database.QueryAsync<Login>("SELECT * FROM [Login]");
		}

		public async Task<IEnumerable<Login>> Get_username_for_delete ()
		{
			return await database.QueryAsync<Login>("SELECT * FROM [Login] WHERE [username] != 'admin'");
		}

		public async Task<IEnumerable<Login>> Delete_username_for_delete ()
		{
			return await database.QueryAsync<Login>("DELETE FROM [Login] WHERE [username] != 'admin'");
		}

		public async Task<IEnumerable<Login>> Check_Login_Table_is_Emtry () 
		{
			return await database.QueryAsync<Login>("SELECT COUNT(*) from [Login]");
		}


		#endregion 

		#region NameByUser
		public async Task<int> Table_is_Emtry2 () 
		{
			return await database.ExecuteAsync("SELECT COUNT([ID]) from [NameByUser]");
		}

		public async Task<IEnumerable<NameByUser>> Table_is_Emtry () 
		{
			return await database.QueryAsync<NameByUser>("SELECT COUNT([ID]) from [NameByUser]");
		}
		public async Task<IEnumerable<NameByUser>> Save_NameByUser_Item (string name, string addr)
		{
			return await database.QueryAsync<NameByUser>(String.Format("INSERT INTO [NameByUser] ([node_addr], [node_name_by_user]) VALUES ({0}, {1})",addr, name ));
		}

		public async Task<IEnumerable<NameByUser>> Update_NameByUser_Item (string name, string addr)
		{
			return await database.QueryAsync<NameByUser>("UPDATE [NameByUser] SET [node_name_by_user] = " + "'" + name + "'" + " WHERE [node_addr] = " + "'" + addr + "'");
		}


		#endregion

		public async Task<IEnumerable<Db_allnode>> GetItems ()
		{
			return await  database.Table<Db_allnode> ().ToListAsync ();
		}

		public async Task<IEnumerable<Db_allnode>> GetItemsNotDone ()
		{
			//lock (locker) {
			return await database.QueryAsync<Db_allnode>("SELECT * FROM [Db_allnode] GROUP BY [node_deviceType]");
			//}
		}

		public async Task<IEnumerable<Db_allnode>> GetItemGroupByDeviceType ()
		{
			return await database.QueryAsync<Db_allnode>("SELECT * FROM [Db_allnode] WHERE [node_deviceType] != 'Unknow2' GROUP BY [node_deviceType]");
		}

		public async Task<IEnumerable<Db_allnode>> GetItemByDeviceType (string deviceType)
		{
			return await database.QueryAsync<Db_allnode>("SELECT * FROM [Db_allnode] WHERE [node_deviceType] = " + "'" + deviceType + "'");
		}

		public async Task<IEnumerable<Db_allnode>> GetIoOfNode (string deviceAddr)
		{
			return await database.QueryAsync<Db_allnode> ("SELECT * FROM [Db_allnode] WHERE [node_addr] = " + "'" + deviceAddr + "'");
		}


		/**public async DeviceDatabaseTable GetItem (int id) 
		{
			//lock (locker) {
				return database.Table<DeviceDatabaseTable>().FirstOrDefault(x => x.ID == id);
			//}
		}**/

		/**public async Task<int> SaveItem (DeviceDatabaseTable item) 
		{
			//lock (locker) {
				if (item.ID != 0) {
					await database.UpdateAsync(item);
					return item.ID;
				} else {
					return await database.InsertAsync(item);
				}
			//}
		}**/

		public async Task<int> Save_DBAllNode_Item (Db_allnode item) 
		{	
			System.Diagnostics.Debug.WriteLine ("Save_DBAllNode_Item");
			return await database.InsertAsync(item);
		}

		//public async Task<int> Update_Item (DeviceDatabaseTable item) 
		//{
			//return await database.UpdateAsync(item);
		//}

		public async Task<int> Update_DBAllNode_Item (Db_allnode item) 
		{
			return await database.UpdateAsync(item);
		}

		public async Task<IEnumerable<Db_allnode>> Update_DBAllNode_All_Item (Db_allnode item) 
		{
			return await database.QueryAsync<Db_allnode>(String.Format("UPDATE [Db_allnode] SET " +
				"[node_io] = \'{0}\', " +
				"[node_status] = \'{1}\', " +
				"[node_type] = \'{2}\', " +
				"[node_deviceType]  = \'{3}\' " +
				"WHERE [node_addr] = \'{4}\'",
				item.node_io, item.node_status, item.node_type, item.node_deviceType, item.node_addr));
		}

		public async Task<IEnumerable<Db_allnode>> Update_DBAllNode_Item2 (string node_status2, int ID2)
		{
			return await database.QueryAsync<Db_allnode>("UPDATE [Db_allnode] SET [node_status] = " + "'" + node_status2 + "'" + " WHERE [ID] = " + ID2);
		}

		public async Task<IEnumerable<Db_allnode>> Update_Node_Io (string node_io, string addr)
		{
			return await database.QueryAsync<Db_allnode>("UPDATE [Db_allnode] SET [node_io] = " + "'" + node_io + "'" + " WHERE [node_addr] = " + "'" + addr + "'");
		}

		public async Task<IEnumerable<Db_allnode>> Update_Node_Io_Node_Status (string node_io, string node_status, string addr)
		{
			//return await database.QueryAsync<Db_allnode>("UPDATE [Db_allnode] SET [node_io] = " + "'" + node_io + "', " + "[node_status] = " + "'" + node_status + "'" + " WHERE [node_addr] = " + "'" + addr + "'");
			return await database.QueryAsync<Db_allnode>(String.Format("UPDATE [Db_allnode] SET [node_io] = \'{0}\' , [node_status] = \'{1}\' WHERE [node_addr] = \'{2}\'",node_io, node_status, addr));
		}

		public async Task<IEnumerable<Db_allnode>> Update_Node_NameByUser(string name, string addr)
		{
			return await database.QueryAsync<Db_allnode>("UPDATE [Db_allnode] SET [name_by_user] = " + "'" + name + "'" + " WHERE [node_addr] = " + "'" + addr + "'");
		}

		/**public  async Task<int> DeleteItem(int id)
		{
			//lock (locker) {
				return await database.DeleteAsync<DeviceDatabaseTable>(id);
			//}
		}**/
	}
}

