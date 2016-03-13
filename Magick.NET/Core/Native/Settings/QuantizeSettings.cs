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
  public partial class QuantizeSettings
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.QuantizeSettings+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QuantizeSettings_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetColors(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetColorSpace(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetDitherMethod(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetMeasureErrors(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetTreeDepth(IntPtr Instance, UIntPtr value);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QuantizeSettings_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetColors(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetColorSpace(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetDitherMethod(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetMeasureErrors(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantizeSettings_SetTreeDepth(IntPtr Instance, UIntPtr value);
      }
    }
    private sealed class NativeQuantizeSettings : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_Dispose(instance);
        else
          NativeMethods.X86.QuantizeSettings_Dispose(instance);
      }
      public NativeQuantizeSettings()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.QuantizeSettings_Create();
        else
          _Instance = NativeMethods.X86.QuantizeSettings_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(QuantizeSettings).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public void SetColors(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_SetColors(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.QuantizeSettings_SetColors(Instance, (UIntPtr)value);
      }
      public void SetColorSpace(ColorSpace value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_SetColorSpace(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.QuantizeSettings_SetColorSpace(Instance, (UIntPtr)value);
      }
      public void SetDitherMethod(DitherMethod value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_SetDitherMethod(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.QuantizeSettings_SetDitherMethod(Instance, (UIntPtr)value);
      }
      public void SetMeasureErrors(bool value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_SetMeasureErrors(Instance, value);
        else
          NativeMethods.X86.QuantizeSettings_SetMeasureErrors(Instance, value);
      }
      public void SetTreeDepth(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.QuantizeSettings_SetTreeDepth(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.QuantizeSettings_SetTreeDepth(Instance, (UIntPtr)value);
      }
    }
    internal static INativeInstance CreateInstance(QuantizeSettings instance)
    {
      if (instance == null)
        return NativeInstance.Zero;
      return instance.CreateNativeInstance();
    }
  }
}
