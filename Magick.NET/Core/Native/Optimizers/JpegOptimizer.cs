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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.ImageOptimizers
{
  public partial class JpegOptimizer
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.JpegOptimizer+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr JpegOptimizer_Optimize(IntPtr input, IntPtr output, [MarshalAs(UnmanagedType.Bool)] bool progressive);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.JpegOptimizer+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr JpegOptimizer_Optimize(IntPtr input, IntPtr output, [MarshalAs(UnmanagedType.Bool)] bool progressive);
      }
    }
    private static class NativeJpegOptimizer
    {
      public static int Optimize(string input, string output, bool progressive)
      {
        using (INativeInstance inputNative = UTF8Marshaler.CreateInstance(input))
        {
          using (INativeInstance outputNative = UTF8Marshaler.CreateInstance(output))
          {
            if (NativeLibrary.Is64Bit)
              return (int)NativeMethods.X64.JpegOptimizer_Optimize(inputNative.Instance, outputNative.Instance, progressive);
            else
              return (int)NativeMethods.X86.JpegOptimizer_Optimize(inputNative.Instance, outputNative.Instance, progressive);
          }
        }
      }
    }
  }
}
