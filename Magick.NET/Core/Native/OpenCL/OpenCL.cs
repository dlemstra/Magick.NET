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

namespace ImageMagick
{
  public static partial class OpenCL
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCL+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCL_GetDevices(out UIntPtr length);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCL_GetInstance(IntPtr list, UIntPtr index);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenCL_SetEnabled([MarshalAs(UnmanagedType.Bool)] bool value);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCL+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCL_GetDevices(out UIntPtr length);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCL_GetInstance(IntPtr list, UIntPtr index);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenCL_SetEnabled([MarshalAs(UnmanagedType.Bool)] bool value);
      }
    }
    private static class NativeOpenCL
    {
      public static IntPtr GetDevices(out UIntPtr length)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.OpenCL_GetDevices(out length);
        else
          return NativeMethods.X86.OpenCL_GetDevices(out length);
      }
      public static IntPtr GetInstance(IntPtr list, int index)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.OpenCL_GetInstance(list, (UIntPtr)index);
        else
          return NativeMethods.X86.OpenCL_GetInstance(list, (UIntPtr)index);
      }
      public static bool SetEnabled(bool value)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.OpenCL_SetEnabled(value);
        else
          return NativeMethods.X86.OpenCL_SetEnabled(value);
      }
    }
  }
}
