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
  public partial class MagickGeometry
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.MagickGeometry+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickGeometry_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickGeometry_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickGeometry_Initialize(IntPtr Instance, IntPtr value);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickGeometry_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickGeometry_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickGeometry_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickGeometry_Initialize(IntPtr Instance, IntPtr value);
      }
    }
    private sealed class NativeMagickGeometry : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickGeometry_Dispose(instance);
        else
          NativeMethods.X86.MagickGeometry_Dispose(instance);
      }
      public NativeMagickGeometry()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.MagickGeometry_Create();
        else
          _Instance = NativeMethods.X86.MagickGeometry_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativeMagickGeometry(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(MagickGeometry).ToString());
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
            result = NativeMethods.X64.MagickGeometry_X_Get(Instance);
          else
            result = NativeMethods.X86.MagickGeometry_X_Get(Instance);
          return result;
        }
      }
      public double Y
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickGeometry_Y_Get(Instance);
          else
            result = NativeMethods.X86.MagickGeometry_Y_Get(Instance);
          return result;
        }
      }
      public double Width
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickGeometry_Width_Get(Instance);
          else
            result = NativeMethods.X86.MagickGeometry_Width_Get(Instance);
          return result;
        }
      }
      public double Height
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickGeometry_Height_Get(Instance);
          else
            result = NativeMethods.X86.MagickGeometry_Height_Get(Instance);
          return result;
        }
      }
      public GeometryFlags Initialize(string value)
      {
        using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
        {
          if (NativeLibrary.Is64Bit)
            return (GeometryFlags)NativeMethods.X64.MagickGeometry_Initialize(Instance, valueNative.Instance);
          else
            return (GeometryFlags)NativeMethods.X86.MagickGeometry_Initialize(Instance, valueNative.Instance);
        }
      }
    }
    internal static MagickGeometry CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;
      using (NativeMagickGeometry nativeInstance = new NativeMagickGeometry(instance))
      {
        return new MagickGeometry(nativeInstance);
      }
    }
  }
}
