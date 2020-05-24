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

using System.Runtime.InteropServices;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to initialize Magick.NET.
    /// </summary>
    public static partial class MagickNET
    {
        /// <summary>
        /// Sets the directory that contains the Native library. This currently only works on Windows.
        /// </summary>
        /// <param name="path">The path of the directory that contains the native library.</param>
        public static void SetNativeLibraryDirectory(string path)
        {
#if NETSTANDARD
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
               NativeWindowsMethods.SetDllDirectory(FileHelper.GetFullPath(path));
#else
            NativeWindowsMethods.SetDllDirectory(FileHelper.GetFullPath(path));
#endif
        }

        private static class NativeWindowsMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetDllDirectory(string lpPathName);
        }
    }
}