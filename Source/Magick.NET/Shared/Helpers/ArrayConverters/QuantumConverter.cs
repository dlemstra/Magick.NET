// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class QuantumConverter
    {
        public static QuantumType[] ToArray(IntPtr nativeData, int length)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            QuantumType[] result = new QuantumType[length];

            unsafe
            {
#if Q8
                byte* sourcePtr = (byte*)nativeData;
#elif Q16
                ushort* sourcePtr = (ushort*)nativeData;
#elif Q16HDRI
                float* sourcePtr = (float*)nativeData;
#else
#error Not implemented!
#endif
                for (int i = 0; i < length; ++i)
                {
                    result[i] = *sourcePtr++;
                }
            }

            return result;
        }
    }
}
