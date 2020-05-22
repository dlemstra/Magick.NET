// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections.Generic;

namespace ImageMagick
{
    internal static partial class MagickColorCollection
    {
        public static void DisposeList(IntPtr list)
        {
            if (list != IntPtr.Zero)
                NativeMagickColorCollection.DisposeList(list);
        }

        public static Dictionary<IMagickColor, int> ToDictionary(IntPtr list, int length)
        {
            var colors = new Dictionary<IMagickColor, int>();

            if (list == IntPtr.Zero)
                return colors;

            for (int i = 0; i < length; i++)
            {
                var instance = NativeMagickColorCollection.GetInstance(list, i);
                DebugThrow.IfNull(instance);

                var color = MagickColor.CreateInstance(instance, out var count);
                colors[color] = count;
            }

            return colors;
        }
    }
}