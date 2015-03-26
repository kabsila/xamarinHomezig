using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class NameByUser
	{
		public NameByUser ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string node_addr { get; set; }
		public string node_name_by_user { get; set; }
		public string io_name_by_user { get; set; }
		public string target_io {get; set;}
	}
}

