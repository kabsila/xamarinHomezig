using System;
using System.Collections.Generic;

namespace HomeZig
{
	public class Db_allnode
	{
		public string node_addr { get; set; }
		public string node_type { get; set; }
		public string node_status { get; set; }


		public string nodeTypeToString {
			get
			{ 
				if (node_type.Equals ("0x3ff90")) {
					node_type = "OutLet";
				} else if (node_type.Equals ("0xa001a")) {
					node_type = "Camera";
				} else {
					node_type = "Unknow";
				}
				return String.Format ("{0}", node_type);
				//return String.Format ("address: {0}, type: {1}", node_addr, node_type);
			}
		}

	}

	public class RootObject
	{
		public List<Db_allnode> cmd_db_allnode { get; set; }
		//public Dictionary<string,List<Db_allnode>> cmd_db_allnode;
		//public Dictionary<string,Db_allnode> cmd_db_allnode;
		//public Db_allnode[] cmd_db_allnode { get; set; }
	}
}

