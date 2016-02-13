//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ImageMagick
{
  internal sealed class UTF8Marshaler : INativeInstance
  {
    private UTF8Marshaler(string value)
    {
      Instance = ManagedToNative(value);
    }

    internal static INativeInstance CreateInstance(string value)
    {
      return new UTF8Marshaler(value);
    }

    internal static IntPtr ManagedToNative(string value)
    {
      if (value == null)
        return IntPtr.Zero;

      // not null terminated
      byte[] strbuf = Encoding.UTF8.GetBytes(value);
      IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length + 1);
      Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

      // write the terminating null
#if NET20
      Marshal.WriteByte(new IntPtr(buffer.ToInt64() + strbuf.Length), 0);
#else
      Marshal.WriteByte(buffer + strbuf.Length, 0);
#endif
      return buffer;
    }

    internal static string NativeToManaged(IntPtr nativeData)
    {
      byte[] strbuf = ByteConverter.ToArray(nativeData);
      if (strbuf == null)
        return null;

      if (strbuf.Length == 0)
        return "";

      return Encoding.UTF8.GetString(strbuf, 0, strbuf.Length);
    }

    internal static string NativeToManagedAndRelinquish(IntPtr nativeData)
    {
      string result = NativeToManaged(nativeData);

      MagickMemory.Relinquish(nativeData);

      return result;
    }

    public IntPtr Instance
    {
      get;
      private set;
    }

    public void Dispose()
    {
      if (Instance == IntPtr.Zero)
        return;

      Marshal.FreeHGlobal(Instance);
      Instance = IntPtr.Zero;
    }
  }

  /*
  [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
  internal class UTF8Marshaler : ICustomMarshaler
  {
    public void CleanUpManagedData(object managedObj)
    {
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
      Marshal.FreeHGlobal(nativeData);
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
      return UTF8MarshalerHelper.MarshalManagedToNative(managedObj);
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
      throw new NotImplementedException();
    }

    public int GetNativeDataSize()
    {
      return -1;
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cookie")]
    public static ICustomMarshaler GetInstance(string cookie)
    {
      return new UTF8Marshaler();
    }
  }*/
}