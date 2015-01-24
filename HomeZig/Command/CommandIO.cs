using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class CommandIo
	{
		public string node_addr { get; set; }
		public string node_command { get; set; }
	}

	public class RootCommandIo
	{
		public List<CommandIo> command_io { get; set; }
	}
}

