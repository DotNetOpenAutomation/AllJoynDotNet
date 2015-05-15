using System;

namespace AboutClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Pervasive Digital AllJoyn Sample - AboutClient");
			var ba = new AllJoynBusAttachment ("door_consumer_c", true);
			ba.Connect ();
		}
	}
}
