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

#if NETSTANDARD
using System.Runtime.InteropServices;
#endif

using System;

namespace ImageMagick
{
    internal static class OperatingSystem
    {
        public static bool Is64Bit =>
            IntPtr.Size == 8;

#if NETSTANDARD
        public static bool IsWindows =>
           RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
#else
        public static bool IsWindows =>
           true;

        public static bool IsMacOS =>
            false;

        public static bool IsLinux =>
            false;
#endif
    }
}
