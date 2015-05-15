using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using PervasiveDigital.AllJoyn.Native;
using System.Reflection;

namespace PervasiveDigital.AllJoyn
{
	public class AllJoynBusAttachment : IDisposable
	{
		private IntPtr _busAttachmentHandle;
		private readonly string _busName;
		private readonly bool _allowRemoteMessages;

		public AllJoynBusAttachment () : this(GenerateBusName(), true)
		{
		}

		public AllJoynBusAttachment(string busName, bool allowRemoteMessages)
		{
			_busName = busName;
			_allowRemoteMessages = allowRemoteMessages;
		}

		~AllJoynBusAttachment()
		{
			Dispose();
		}

		public void Dispose()
		{
			Disconnect ();
		}

		public string UniqueName
		{
			get
			{
				if (_busAttachmentHandle == IntPtr.Zero)
					throw new InvalidOperationException ("Bus is not connected");
				var ptr = AllJoynNative.alljoyn_busattachment_getuniquename (_busAttachmentHandle);
				var result = PtrToStringUtf8 (ptr);
				//TODO: free ptr
				return result;
			}
		}

		public void Connect()
		{
			_busAttachmentHandle = AllJoynNative.alljoyn_busattachment_create (_busName, AllJoynNative.QCC_BOOL(_allowRemoteMessages));
			var result = AllJoynNative.alljoyn_busattachment_start (_busAttachmentHandle);
			if (result != 0)
				throw new Exception ("Failed to start bus attachment. Err=" + result);
			result = AllJoynNative.alljoyn_busattachment_connect(_busAttachmentHandle, null);
			if (result!=0)
				throw new Exception ("Failed to connect bus attachment. Err=" + result);
			Debug.WriteLine ("BusAttachment connect succeeded. Bus name = " + _busName);
			var handle = AllJoynNative.alljoyn_aboutlistener_create (ListenerCallback, IntPtr.Zero);
		}

		public void Disconnect()
		{
			if (_busAttachmentHandle != IntPtr.Zero) {
				var result = AllJoynNative.alljoyn_busattachment_disconnect (_busAttachmentHandle, null);
				if (result != 0)
					throw new Exception ("Failed to disconnect bus attachment. Err=" + result);
				_busAttachmentHandle = IntPtr.Zero;
			}
		}

		private static string GenerateBusName()
		{
			var assy = Assembly.GetEntryAssembly ();
			if (assy == null)
				assy = Assembly.GetCallingAssembly ();

			var result = assy.GetName ().Name;
			return result;
		}

		private static void ListenerCallback (
			IntPtr context,
			[MarshalAs (UnmanagedType.LPStr)] string busName,
			UInt16 version,
			IntPtr port, // alljoyn_sessionport port
			IntPtr objectDescriptionArg, // alljoyn_msgarg
			IntPtr aboutDataArg // alljoyn_msgarg
		)
		{
			Debug.WriteLine ("hello there");
		}

		private static string PtrToStringUtf8(IntPtr ptr) // aPtr is nul-terminated
		{
			if (ptr == IntPtr.Zero)
				return "";
			int len = 0;
			while (System.Runtime.InteropServices.Marshal.ReadByte(ptr, len) != 0)
				len++;
			if (len == 0)
				return "";
			byte[] array = new byte[len];
			System.Runtime.InteropServices.Marshal.Copy(ptr, array, 0, len);
			return System.Text.Encoding.UTF8.GetString(array);
		}
	}
}

