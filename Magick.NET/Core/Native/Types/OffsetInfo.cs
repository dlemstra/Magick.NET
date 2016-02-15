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
  internal partial class OffsetInfo
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OffsetInfo_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_SetX(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_SetY(IntPtr Instance, UIntPtr value);
      }
      public static class X86
      {
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OffsetInfo_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_SetX(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OffsetInfo_SetY(IntPtr Instance, UIntPtr value);
      }
    }
    private sealed class NativeOffsetInfo : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.OffsetInfo_Dispose(instance);
        else
          NativeMethods.X86.OffsetInfo_Dispose(instance);
      }
      public NativeOffsetInfo()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.OffsetInfo_Create();
        else
          _Instance = NativeMethods.X86.OffsetInfo_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(OffsetInfo).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public void SetX(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.OffsetInfo_SetX(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.OffsetInfo_SetX(Instance, (UIntPtr)value);
      }
      public void SetY(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.OffsetInfo_SetY(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.OffsetInfo_SetY(Instance, (UIntPtr)value);
      }
    }
    internal static INativeInstance CreateInstance(OffsetInfo instance)
    {
      if (instance == null)
        return NativeInstance.Zero;
      return instance.CreateNativeInstance();
    }
  }
}
