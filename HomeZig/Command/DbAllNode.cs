using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class Db_allnode
	{
		string ns, nd, nc, nbu;
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
		public string node_io_p { get; set; }
		public string name_by_user 
		{
			get
			{ 
				return nbu;
				//return String.Format ("{0}", node_addr);
			}
			set
			{
				nbu = value;
			} 
		}

		public string node_command 
		{
			get
			{ 
				return nc;
			}
			set
			{
				nc = value;
			}
		}

		public string node_deviceType {
			get
			{ 
				if (node_type.Equals ("0x3FF01") || node_type.Equals ("0x3ff01")) {
					nd = "In wall switch";
				}else if (node_type.Equals ("0xA001C") || node_type.Equals ("0xa001c")) {
					nd = "Camera";
				}else if (node_type.Equals ("0x3FF11") || node_type.Equals ("0x3ff11")) {
					nd = "General purpose detector";
				}else if (node_type.Equals ("0x3FF20") || node_type.Equals ("0x3ff20")) {
					nd = "Remote control";
				}
				else {
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
					ns = "false";
				} else {
					ns = "true";
				}
				return String.Format ("{0}", ns);
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
		public List<Login> cmd_login { get; set; }
		public List<RemoteData> cmd_remote { get; set; }
		//public Dictionary<string,List<Db_allnode>> cmd_db_allnode;
		//public Dictionary<string,Db_allnode> cmd_db_allnode;
		//public Db_allnode[] cmd_db_allnode { get; set; }
	}
}

