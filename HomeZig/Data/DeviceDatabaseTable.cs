using System;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class DeviceDatabaseTable
	{
		public DeviceDatabaseTable ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string DeviceType { get; set; }
		public string Address { get; set; }
		public string Name { get; set; }

	}
}

