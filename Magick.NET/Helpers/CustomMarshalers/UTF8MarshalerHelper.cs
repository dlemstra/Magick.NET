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
  internal static class UTF8MarshalerHelper
  {
    internal static IntPtr MarshalManagedToNative(object managedObj)
    {
      if (managedObj == null)
        return IntPtr.Zero;

      if (!(managedObj is string))
        throw new InvalidProgramException();

      // not null terminated
      byte[] strbuf = Encoding.UTF8.GetBytes((string)managedObj);
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

    internal static string MarshalNativeToManaged(IntPtr nativeData)
    {
      byte[] strbuf = ByteConverter.ToArray(nativeData);
      if (strbuf == null)
        return null;

      if (strbuf.Length == 0)
        return "";

      return Encoding.UTF8.GetString(strbuf);
    }
  }
}
