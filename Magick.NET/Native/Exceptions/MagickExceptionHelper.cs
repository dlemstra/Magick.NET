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
  internal static partial class MagickExceptionHelper
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickExceptionHelper_Description(IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickExceptionHelper_Dispose(IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickExceptionHelper_Message(IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickExceptionHelper_Related(IntPtr exception, UIntPtr index);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickExceptionHelper_RelatedCount(IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickExceptionHelper_Severity(IntPtr exception);
      }
      public static class X86
      {
        static X86() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickExceptionHelper_Description(IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickExceptionHelper_Dispose(IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickExceptionHelper_Message(IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickExceptionHelper_Related(IntPtr exception, UIntPtr index);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickExceptionHelper_RelatedCount(IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickExceptionHelper_Severity(IntPtr exception);
      }
      public static string Description(IntPtr exception)
      {
        if (NativeLibrary.Is64Bit)
          return X64.MagickExceptionHelper_Description(exception);
        else
          return X86.MagickExceptionHelper_Description(exception);
      }
      public static void Dispose(IntPtr exception)
      {
        if (NativeLibrary.Is64Bit)
          X64.MagickExceptionHelper_Dispose(exception);
        else
          X86.MagickExceptionHelper_Dispose(exception);
      }
      public static string Message(IntPtr exception)
      {
        if (NativeLibrary.Is64Bit)
          return X64.MagickExceptionHelper_Message(exception);
        else
          return X86.MagickExceptionHelper_Message(exception);
      }
      public static IntPtr Related(IntPtr exception, int index)
      {
        if (NativeLibrary.Is64Bit)
          return X64.MagickExceptionHelper_Related(exception, (UIntPtr)index);
        else
          return X86.MagickExceptionHelper_Related(exception, (UIntPtr)index);
      }
      public static int RelatedCount(IntPtr exception)
      {
        if (NativeLibrary.Is64Bit)
          return (int)X64.MagickExceptionHelper_RelatedCount(exception);
        else
          return (int)X86.MagickExceptionHelper_RelatedCount(exception);
      }
      public static int Severity(IntPtr exception)
      {
        if (NativeLibrary.Is64Bit)
          return (int)X64.MagickExceptionHelper_Severity(exception);
        else
          return (int)X86.MagickExceptionHelper_Severity(exception);
      }
    }
  }
}
