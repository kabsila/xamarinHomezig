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
		public string NameByUserNodeOfProfile { get; set; }
		public string nodeAddrOfProfile { get; set; }
		public string node_deviceType { get; set; }
		public string node_command { get; set; }
		public string profile_status { get; set; }
	}
}

