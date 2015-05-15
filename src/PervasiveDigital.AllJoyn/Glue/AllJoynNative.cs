using System;
using System.Runtime.InteropServices;

namespace PervasiveDigital.AllJoyn.Native
{
	internal struct alljoyn_aboutlistener_callback
	{
		[MarshalAs(UnmanagedType.FunctionPtr)]
		IntPtr about_listener_announced;
	}
	public delegate void AllJoynListenerCallback(
		IntPtr context,
		[MarshalAs(UnmanagedType.LPStr)] string busName,
		UInt16 version,
		IntPtr port, // alljoyn_sessionport port
		IntPtr objectDescriptionArg, // alljoyn_msgarg
		IntPtr aboutDataArg // alljoyn_msgarg
		);

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

		[DllImport(LIBALLJOYN)]
		public static extern IntPtr alljoyn_busattachment_getuniquename (IntPtr bus);

		[DllImport(LIBALLJOYN)]
		public static extern Int32 alljoyn_busattachment_disconnect(IntPtr bus, [MarshalAs(UnmanagedType.LPStr)] string unused);

		[DllImport(LIBALLJOYN)]
		public static extern Int32 alljoyn_busattachment_createinterface(IntPtr bus, [MarshalAs(UnmanagedType.LPStr)] string name, ref IntPtr ifaceHandle);

		// About
		[DllImport(LIBALLJOYN)]
		public static extern IntPtr /*alljoyn_aboutlistener*/ alljoyn_aboutlistener_create(AllJoynListenerCallback callback, IntPtr context);

		[DllImport(LIBALLJOYN)]
		public static extern void alljoyn_aboutlistener_destroy(IntPtr listenerHandle);
	}
}

