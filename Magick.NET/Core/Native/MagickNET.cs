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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void LogDelegate(UIntPtr type, IntPtr value);
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickNET_Features_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetEnv(IntPtr name, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogDelegate(LogDelegate method);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogEvents(IntPtr events);
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
        public static extern IntPtr MagickNET_Features_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetEnv(IntPtr name, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogDelegate(LogDelegate method);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetLogEvents(IntPtr events);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickNET_SetRandomSeed(long value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickNET_SetUseOpenCL([MarshalAs(UnmanagedType.Bool)] bool value, out IntPtr exception);
      }
    }
    private static class NativeMagickNET
    {
      public static string Features
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickNET_Features_Get();
          else
            result = NativeMethods.X86.MagickNET_Features_Get();
          return UTF8Marshaler.NativeToManaged(result);
        }
      }
      public static void SetEnv(string name, string value)
      {
        using (INativeInstance nameNative = UTF8Marshaler.CreateInstance(name))
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickNET_SetEnv(nameNative.Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickNET_SetEnv(nameNative.Instance, valueNative.Instance);
          }
        }
      }
      public static void SetLogDelegate(LogDelegate method)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickNET_SetLogDelegate(method);
        else
          NativeMethods.X86.MagickNET_SetLogDelegate(method);
      }
      public static void SetLogEvents(string events)
      {
        using (INativeInstance eventsNative = UTF8Marshaler.CreateInstance(events))
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickNET_SetLogEvents(eventsNative.Instance);
          else
            NativeMethods.X86.MagickNET_SetLogEvents(eventsNative.Instance);
        }
      }
      public static void SetRandomSeed(long value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickNET_SetRandomSeed(value);
        else
          NativeMethods.X86.MagickNET_SetRandomSeed(value);
      }
      public static bool SetUseOpenCL(bool value)
      {
        IntPtr exception = IntPtr.Zero;
        bool result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickNET_SetUseOpenCL(value, out exception);
        else
          result = NativeMethods.X86.MagickNET_SetUseOpenCL(value, out exception);
        MagickExceptionHelper.Check(exception);
        return result;
      }
    }
  }
}
