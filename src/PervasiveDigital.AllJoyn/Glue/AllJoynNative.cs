using System;
using System.Runtime.InteropServices;

namespace PervasiveDigital.AllJoyn.Native
{
	internal static class AllJoynNative
	{
		private const string LIBALLJOYN = "alljoyn_c"; // will trigger a search for liballjoyn.so

		public const Int32 QCC_TRUE = 1;
		public const Int32 QCC_FALSE = 0;

		static AllJoynNative()
		{
			var result = alljoyn_init();
			if (result!=0)
				throw new Exception("AllJoyn library initialization failed");
		}

		public static Int32 QCC_BOOL(bool flag)
		{
			return flag ? QCC_TRUE : QCC_FALSE;
		}

		[DllImport(LIBALLJOYN)]
		public static extern Int32 alljoyn_init ();

		[DllImport(LIBALLJOYN)]
		public static extern IntPtr alljoyn_busattachment_create(
			[MarshalAs(UnmanagedType.LPStr)]string applicationName, 
			Int32 allowRemoteMessages);

		[DllImport(LIBALLJOYN)]
		public static extern Int32 alljoyn_busattachment_start (IntPtr busHandle);

		[DllImport(LIBALLJOYN)]
		public static extern Int32 alljoyn_busattachment_connect(IntPtr bus, [MarshalAs(UnmanagedType.LPStr)] string connectSpec);
	}
}

