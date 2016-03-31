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
  internal partial class MagickRectangle
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.MagickRectangle+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_X_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Y_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickRectangle_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Width_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickRectangle_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Height_Set(IntPtr instance, UIntPtr value);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.MagickRectangle+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_X_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickRectangle_Y_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Y_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickRectangle_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Width_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickRectangle_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickRectangle_Height_Set(IntPtr instance, UIntPtr value);
      }
    }
    private sealed class NativeMagickRectangle : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickRectangle_Dispose(instance);
        else
          NativeMethods.X86.MagickRectangle_Dispose(instance);
      }
      public NativeMagickRectangle()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.MagickRectangle_Create();
        else
          _Instance = NativeMethods.X86.MagickRectangle_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativeMagickRectangle(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(MagickRectangle).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public int X
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickRectangle_X_Get(Instance);
          else
            result = NativeMethods.X86.MagickRectangle_X_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickRectangle_X_Set(Instance, (IntPtr)value);
          else
            NativeMethods.X86.MagickRectangle_X_Set(Instance, (IntPtr)value);
        }
      }
      public int Y
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickRectangle_Y_Get(Instance);
          else
            result = NativeMethods.X86.MagickRectangle_Y_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickRectangle_Y_Set(Instance, (IntPtr)value);
          else
            NativeMethods.X86.MagickRectangle_Y_Set(Instance, (IntPtr)value);
        }
      }
      public int Width
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickRectangle_Width_Get(Instance);
          else
            result = NativeMethods.X86.MagickRectangle_Width_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickRectangle_Width_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickRectangle_Width_Set(Instance, (UIntPtr)value);
        }
      }
      public int Height
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickRectangle_Height_Get(Instance);
          else
            result = NativeMethods.X86.MagickRectangle_Height_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickRectangle_Height_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickRectangle_Height_Set(Instance, (UIntPtr)value);
        }
      }
    }
    internal static INativeInstance CreateInstance(MagickRectangle instance)
    {
      if (instance == null)
        return NativeInstance.Zero;
      return instance.CreateNativeInstance();
    }
    internal static MagickRectangle CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;
      using (NativeMagickRectangle nativeInstance = new NativeMagickRectangle(instance))
      {
        return new MagickRectangle(nativeInstance);
      }
    }
  }
}
