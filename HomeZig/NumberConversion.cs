using System;

namespace HomeZig
{
	public class NumberConversion
	{
		public static string hex2binary(string hexvalue)
		{
			string binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
			return binaryval;
		}

		public static string binary2hex(string strBinary)
		{
			string strHex = Convert.ToInt32(strBinary,2).ToString("X");
			return strHex;
		}
	}
}

