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
			database.CreateTableAsync<Db_allnode>();
			database.CreateTableAsync<NameByUser>();
		}

		#region NameByUser
		public async Task<int> Save_NameByUser_Item (NameByUser item) 
		{	
			System.Diagnostics.Debug.WriteLine ("Save_NameByUser_Item");
			return await database.InsertAsync(item);
		}
		#endregion

		public async Task<IEnumerable<Db_allnode>> GetItems ()
		{
			//lock (locker) {
			//return  await (from i in database.Table<DeviceDatabaseTable>() select i).ToList();
			//}
			//var aa = database.Table<Db_allnode> ().ToListAsync ();
			return await  database.Table<Db_allnode> ().ToListAsync ();
			//return await database.Table<DeviceDatabaseTable>().ToListAsync<IEnumerable<DeviceDatabaseTable>>();
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

		public async Task<int> SaveItem (DeviceDatabaseTable item) 
		{
			//lock (locker) {
				if (item.ID != 0) {
					await database.UpdateAsync(item);
					return item.ID;
				} else {
					return await database.InsertAsync(item);
				}
			//}
		}

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

		public  async Task<int> DeleteItem(int id)
		{
			//lock (locker) {
				return await database.DeleteAsync<DeviceDatabaseTable>(id);
			//}
		}
	}
}

