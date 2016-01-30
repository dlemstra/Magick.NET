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
  public static partial class MagickNET
  {
    private static class NativeMethods
    {
      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      public delegate void LogDelegate(UIntPtr type, IntPtr value);
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickNET_Features_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetEnv([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogDelegate(LogDelegate method);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogEvents([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string events);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetRandomSeed(long value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickNET_SetUseOpenCL([MarshalAs(UnmanagedType.Bool)] bool value, out IntPtr exception);
      }
      public static class X86
      {
        static X86() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickNET_Features_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetEnv([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogDelegate(LogDelegate method);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogEvents([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string events);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetRandomSeed(long value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickNET_SetUseOpenCL([MarshalAs(UnmanagedType.Bool)] bool value, out IntPtr exception);
      }
      public static string Features
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.MagickNET_Features_Get();
          else
            return X86.MagickNET_Features_Get();
        }
      }
      public static void SetEnv(string name, string value)
      {
        if (NativeLibrary.Is64Bit)
          X64.MagickNET_SetEnv(name, value);
        else
          X86.MagickNET_SetEnv(name, value);
      }
      public static void SetLogDelegate(LogDelegate method)
      {
        if (NativeLibrary.Is64Bit)
          X64.MagickNET_SetLogDelegate(method);
        else
          X86.MagickNET_SetLogDelegate(method);
      }
      public static void SetLogEvents(string events)
      {
        if (NativeLibrary.Is64Bit)
          X64.MagickNET_SetLogEvents(events);
        else
          X86.MagickNET_SetLogEvents(events);
      }
      public static void SetRandomSeed(long value)
      {
        if (NativeLibrary.Is64Bit)
          X64.MagickNET_SetRandomSeed(value);
        else
          X86.MagickNET_SetRandomSeed(value);
      }
      public static bool SetUseOpenCL(bool value)
      {
        IntPtr exception = IntPtr.Zero;
        bool result;
        if (NativeLibrary.Is64Bit)
          result = X64.MagickNET_SetUseOpenCL(value, out exception);
        else
          result = X86.MagickNET_SetUseOpenCL(value, out exception);
        MagickExceptionHelper.Check(exception);
        return result;
      }
    }
  }
}
