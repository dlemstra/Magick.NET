// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    internal static class NativeLibrary
    {
#if Q8

#if PLATFORM_x86 || PLATFORM_AnyCPU
        public const string X86Name = "Magick.Native-Q8-x86.dll";
#endif

#if PLATFORM_x64 || PLATFORM_AnyCPU

#if OPENMP
        public const string X64Name = "Magick.Native-Q8-OpenMP-x64.dll";
#else
        public const string X64Name = "Magick.Native-Q8-x64.dll";
#endif

#endif

#elif Q16

#if PLATFORM_x86 || PLATFORM_AnyCPU
        public const string X86Name = "Magick.Native-Q16-x86.dll";
#endif

#if PLATFORM_x64 || PLATFORM_AnyCPU

#if OPENMP
        public const string X64Name = "Magick.Native-Q16-OpenMP-x64.dll";
#else
        public const string X64Name = "Magick.Native-Q16-x64.dll";
#endif

#endif

#elif Q16HDRI

#if PLATFORM_x86 || PLATFORM_AnyCPU
        public const string X86Name = "Magick.Native-Q16-HDRI-x86.dll";
#endif

#if PLATFORM_x64 || PLATFORM_AnyCPU

#if OPENMP
        public const string X64Name = "Magick.Native-Q16-HDRI-OpenMP-x64.dll";
#else
        public const string X64Name = "Magick.Native-Q16-HDRI-x64.dll";
#endif

#endif

#else
#error Not implemented!
#endif

#if PLATFORM_AnyCPU
        public static bool Is64Bit
        {
            get
            {
                return IntPtr.Size == 8;
            }
        }
#endif
    }
}
