using System;
using SQLite.Net;
using SQLite.Net.Async;

namespace HomeZig
{
	public interface ISQLite
	{
		SQLiteAsyncConnection GetConnection();
	}
}

