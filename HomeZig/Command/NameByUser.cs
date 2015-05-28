﻿using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace HomeZig
{
	public class NameByUser
	{
		string ni;
		public NameByUser ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string node_addr { get; set; }
		public string node_name_by_user { get; set; }
		public string io_name_by_user { get; set; }
		public string node_io { get; set; }

		public string io_value 
		{
			get 
			{ 
				string state = NumberConversion.hex2binary (node_io);
				string io_state = string.Empty;
				if(target_io.Equals("1")){
					io_state = state.Substring(4, 1);
					if (io_state.Equals ("0")) 
					{ 
						io_state = "true";
					} 
					else 
					{
						io_state = "false";
					}
				}else if(target_io.Equals("2")){
					io_state = state.Substring(5, 1);
					if (io_state.Equals ("0")) 
					{ 
						io_state = "true";
					} 
					else 
					{
						io_state = "false";
					}
				}else if(target_io.Equals("3")){
					io_state = state.Substring(6, 1);
					if (io_state.Equals ("0")) 
					{ 
						io_state = "true";
					} 
					else 
					{
						io_state = "false";
					}
				}else if(target_io.Equals("4")){
					io_state = state.Substring(7, 1);
					if (io_state.Equals ("0")) 
					{ 
						io_state = "true";
					} 
					else 
					{
						io_state = "false";
					}
				}				

				return String.Format ("{0}", io_state);

			}
			set 
			{
				ni = value;
			}
		}
		public string target_io {get; set;}
		//public string this_is_togle {get; set;}


	}
}

