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
  public partial class PrimaryInfo
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PrimaryInfo+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PrimaryInfo_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_X_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Y_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_Z_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Z_Set(IntPtr instance, double value);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PrimaryInfo+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PrimaryInfo_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_X_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Y_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PrimaryInfo_Z_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrimaryInfo_Z_Set(IntPtr instance, double value);
      }
    }
    private sealed class NativePrimaryInfo : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.PrimaryInfo_Dispose(instance);
        else
          NativeMethods.X86.PrimaryInfo_Dispose(instance);
      }
      public NativePrimaryInfo()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.PrimaryInfo_Create();
        else
          _Instance = NativeMethods.X86.PrimaryInfo_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativePrimaryInfo(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(PrimaryInfo).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public double X
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PrimaryInfo_X_Get(Instance);
          else
            result = NativeMethods.X86.PrimaryInfo_X_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.PrimaryInfo_X_Set(Instance, value);
          else
            NativeMethods.X86.PrimaryInfo_X_Set(Instance, value);
        }
      }
      public double Y
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PrimaryInfo_Y_Get(Instance);
          else
            result = NativeMethods.X86.PrimaryInfo_Y_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.PrimaryInfo_Y_Set(Instance, value);
          else
            NativeMethods.X86.PrimaryInfo_Y_Set(Instance, value);
        }
      }
      public double Z
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PrimaryInfo_Z_Get(Instance);
          else
            result = NativeMethods.X86.PrimaryInfo_Z_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.PrimaryInfo_Z_Set(Instance, value);
          else
            NativeMethods.X86.PrimaryInfo_Z_Set(Instance, value);
        }
      }
    }
    internal static INativeInstance CreateInstance(PrimaryInfo instance)
    {
      if (instance == null)
        return NativeInstance.Zero;
      return instance.CreateNativeInstance();
    }
    internal static PrimaryInfo CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;
      using (NativePrimaryInfo nativeInstance = new NativePrimaryInfo(instance))
      {
        return new PrimaryInfo(nativeInstance);
      }
    }
  }
}
