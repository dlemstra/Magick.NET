// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick
{
    internal static class ByteConverter
    {
        public static byte[] ToArray(IntPtr nativeData)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            unsafe
            {
                int length = 0;
                byte* walk = (byte*)nativeData;

                // find the end of the string
                while (*(walk++) != 0)
                    length++;

                if (length == 0)
                    return new byte[0];

                return ToArray(nativeData, length);
            }
        }

        public static byte[] ToArray(IntPtr nativeData, int length)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            byte[] buffer = new byte[length];
            Marshal.Copy(nativeData, buffer, 0, length);
            return buffer;
        }
    }
}
