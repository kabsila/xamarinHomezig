using System;
using System.Collections.Generic;


namespace HomeZig
{
	public class Outlet
	{
		public string node_addr { get; set; }
		public string node_status { get; set; }

		public string nodeStatusToString 
		{ 
			get
			{
				if (node_status.Equals ("0")) { 
					node_status = "false";
				} else {
					node_status = "true";
				}
				return String.Format ("{0}", node_status);
			} 
		}

		public string node_deviceType 
		{ 
			get
			{
				return String.Format ("{0}", this.GetType ().Name);
			} 
		}


	}

	public class Camera
	{
		public string node_addr { get; set; }
		public string node_status { get; set; }

		public string nodeStatusToString 
		{ 
			get
			{
				if (node_status.Equals ("0")) { 
					node_status = "false";
				} else {
					node_status = "true";
				}
				return String.Format ("{0}", node_status);
			} 
		}

		public string node_deviceType 
		{ 
			get
			{
				return String.Format ("{0}", this.GetType ().Name);
			} 
		}
	}

	public class CmdDbAllnode
	{
		public List<Outlet> Outlet { get; set; }
		public List<Camera> Camera { get; set; }
	}

	public class RootElement
	{
		public List<CmdDbAllnode> cmd_db_allnode { get; set; }
	}



}

