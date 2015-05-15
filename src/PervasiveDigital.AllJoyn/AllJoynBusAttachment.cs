using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using PervasiveDigital.AllJoyn.Native;

namespace PervasiveDigital.AllJoyn
{
	public class AllJoynBusAttachment
	{
		private IntPtr _busAttachmentHandle;

		public AllJoynBusAttachment ()
		{
		}

		public AllJoynBusAttachment(string busName, bool allowRemoteMessages)
		{
			_busAttachmentHandle = AllJoynNative.alljoyn_busattachment_create (busName, AllJoynNative.QCC_BOOL(allowRemoteMessages));
			Debug.WriteLine ("result = " + _busAttachmentHandle);
			var result = AllJoynNative.alljoyn_busattachment_start (_busAttachmentHandle);
			if (result != 0)
				throw new Exception ("Failed to start bus attachment. Err=" + result);
		}

		public void Connect()
		{
			var result = AllJoynNative.alljoyn_busattachment_connect(_busAttachmentHandle, null);
			if (result!=0)
				throw new Exception ("Failed to start bus attachment. Err=" + result);
		}

		public void Disconnect()
		{
		}
	}
}

