using System;
using System.Diagnostics;

using PervasiveDigital.AllJoyn;

namespace DoorConsumer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Pervasive Digital AllJoyn Sample - Door Consumer");
			var ba = new AllJoynBusAttachment ();
			ba.Connect ();
			Debug.WriteLine ("Unique name : " + ba.UniqueName);

			while (true)
				System.Threading.Thread.Sleep (500);
		}
	}
}
