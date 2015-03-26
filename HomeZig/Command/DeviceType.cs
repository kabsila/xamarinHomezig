using System;

namespace HomeZig
{
	public enum DeviceType
	{
		InWallSwitch,
		Camera,
		GeneralPurposeDetector,
		RemoteControl,
		UnknowDeviceType
	};

	public class EnumtoString
	{
		public EnumtoString()
		{

		}

		public static string EnumString(Enum deviceType){

			string eString = string.Empty;
			if (deviceType.Equals (DeviceType.InWallSwitch)) {
				eString = "In Wall Swtich";
			} else if (deviceType.Equals (DeviceType.Camera)) {
				eString = "Camera";
			} else if (deviceType.Equals (DeviceType.GeneralPurposeDetector)) {
				eString = "General Purpose Detector";
			} else if (deviceType.Equals (DeviceType.RemoteControl)) {
				eString = "Remote Control";
			} else {
				eString = "UnknowDeviceType";
			}

			return eString;
		}
	}
}

