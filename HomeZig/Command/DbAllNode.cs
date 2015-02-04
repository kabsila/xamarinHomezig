﻿using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;



namespace HomeZig
{
	public class Db_allnode
	{
		string ns, nd, nt;
		public Db_allnode ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[Unique]
		public string node_addr { get; set; }
		public string node_type { get; set; }
		public string node_status { get; set; }
		public string node_io { get; set; }
		public string node_command { get; set; }

		public string node_deviceType {
			get
			{ 
				if (node_type.Equals ("0x3ff90")) {
					nd = "OutLet";
				} else if (node_type.Equals ("0xa001a")) {
					nd = "Camera";
				} else {
					nd = "Unknow2";
				}
				return String.Format ("{0}", nd);
				//return String.Format ("address: {0}, type: {1}", node_addr, node_type);
			}
			set
			{
				nd = value;
			}
		}

		public string nodeStatusToString 
		{ 
			get
			{
				if (node_status.Equals ("0") || node_status.Equals ("false") || node_status.Equals ("False")) { 
					node_status = "false";
				} else {
					node_status = "true";
				}
				return String.Format ("{0}", node_status);
			} 
			set
			{
				ns = value;
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

