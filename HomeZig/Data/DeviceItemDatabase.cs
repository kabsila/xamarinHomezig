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
		static object locker = new object ();

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
		}


		public async Task<IEnumerable<Db_allnode>> GetItems ()
		{
			//lock (locker) {
			//return  await (from i in database.Table<DeviceDatabaseTable>() select i).ToList();
			//}
			var aa = database.Table<Db_allnode> ().ToListAsync ();
			return await aa;
			//return await database.Table<DeviceDatabaseTable>().ToListAsync<IEnumerable<DeviceDatabaseTable>>();
		}

		public async Task<IEnumerable<DeviceDatabaseTable>> GetItemsNotDone ()
		{
			//lock (locker) {
				return await database.QueryAsync<DeviceDatabaseTable>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
			//}
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
			//lock (locker) {
			//if (item.ID != 0) {
				//await database.UpdateAsync(item);
				//return item.ID;
				//return await database.UpdateAsync(item);
			//} else {
				
			return await database.InsertAsync(item);
			//}
			//}
		}


		public  async Task<int> DeleteItem(int id)
		{
			//lock (locker) {
				return await database.DeleteAsync<DeviceDatabaseTable>(id);
			//}
		}
	}
}

