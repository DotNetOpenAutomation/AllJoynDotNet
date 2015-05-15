using System;
using PervasiveDigital.AllJoyn;

namespace DoorConsumer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Pervasive Digital AllJoyn Sample - Door Consumer");
			var ba = new AllJoynBusAttachment ("door_consumer_c", true);
			ba.Connect ();
		}
	}
}
