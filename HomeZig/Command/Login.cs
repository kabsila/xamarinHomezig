using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class Login
	{
		public Login ()
		{
		}

		public Login (string username2)
		{
			this.username = username2;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string lastConnectWebscoketUrl { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string flagForLogin { get; set; }
		public string node_command { get; set; }
	}
}

