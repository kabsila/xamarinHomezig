using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class ProfileData
	{
		public ProfileData ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string profileName { get; set; }
		//public string username { get; set; }
		//public string password { get; set; }
		//public string flagForLogin { get; set; }
		//public string node_command { get; set; }
	}
}

