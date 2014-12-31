using System;
using System.Collections.Generic;

namespace HomeZig
{
	public class Db_allnode
	{

		public string node_addr { get; set; }
		public string node_type { get; set; }
		public string node_status { get; set; }
	}

	public class RootObject
	{
		public IList<Db_allnode> cmd_db_allnode { get; set; }
	}
}

