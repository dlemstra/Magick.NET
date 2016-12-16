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
using System.IO;
using System.Runtime.InteropServices;

namespace ImageMagick
{
  internal static partial class MagickMemory
  {
    private static void Write(IntPtr value, int length, Stream stream)
    {
      if (length == 0)
        return;

      int bufferSize = Math.Min(length, 8192);
      byte[] buffer = new byte[bufferSize];

      int offset = 0;
      IntPtr ptr = value;
      while (offset < length)
      {
        int count = (offset + bufferSize > length) ? length - offset : bufferSize;

        Marshal.Copy(ptr, buffer, 0, count);

        stream.Write(buffer, 0, count);

        offset += bufferSize;
        ptr = new IntPtr(ptr.ToInt64() + count);
      }
    }

    public static void Relinquish(IntPtr value)
    {
      NativeMagickMemory.Relinquish(value);
    }

    public static void WriteBytes(IntPtr value, UIntPtr length, Stream stream)
    {
      if (value == IntPtr.Zero)
        return;

      try
      {
        Write(value, (int)length, stream);
      }
      finally
      {
        Relinquish(value);
      }
    }
  }
}
