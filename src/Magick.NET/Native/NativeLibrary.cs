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

namespace ImageMagick
{
    internal static class NativeLibrary
    {
        public const string Name = "Magick.Native";

        public const string QuantumName = Quantum + OpenMP;

        public const string X86Name = Name + "-" + QuantumName + "-x86.dll";

        public const string X64Name = Name + "-" + QuantumName + "-x64.dll";

#if Q8
        private const string Quantum = "Q8";
#elif Q16
        private const string Quantum = "Q16";
#elif Q16HDRI
        private const string Quantum = "Q16-HDRI";
#else
#error Not implemented!
#endif

#if OPENMP
        private const string OpenMP = "-OpenMP";
#else
        private const string OpenMP = "";
#endif

#if PLATFORM_AnyCPU
        public static string PlatformName => Is64Bit ? "x64" : "x86";

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
