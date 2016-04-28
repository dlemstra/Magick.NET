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
  public partial class PixelCollection : IDisposable
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PixelCollection+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_Create(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PixelCollection_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_GetArea(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PixelCollection_SetArea(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, QuantumType[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_ToByteArray(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, IntPtr mapping, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_ToShortArray(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, IntPtr mapping, out IntPtr exception);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PixelCollection+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_Create(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PixelCollection_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_GetArea(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PixelCollection_SetArea(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, QuantumType[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_ToByteArray(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, IntPtr mapping, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PixelCollection_ToShortArray(IntPtr Instance, UIntPtr x, UIntPtr y, UIntPtr width, UIntPtr height, IntPtr mapping, out IntPtr exception);
      }
    }
    private NativePixelCollection _NativeInstance;
    private sealed class NativePixelCollection : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.PixelCollection_Dispose(instance);
        else
          NativeMethods.X86.PixelCollection_Dispose(instance);
      }
      public NativePixelCollection(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.PixelCollection_Create(MagickImage.GetInstance(image), out exception);
        else
          _Instance = NativeMethods.X86.PixelCollection_Create(MagickImage.GetInstance(image), out exception);
        CheckException(exception, _Instance);
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(PixelCollection).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public IntPtr GetArea(int x, int y, int width, int height)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.PixelCollection_GetArea(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, out exception);
        else
          result = NativeMethods.X86.PixelCollection_GetArea(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, out exception);
        CheckException(exception);
        return result;
      }
      public void SetArea(int x, int y, int width, int height, QuantumType[] values, int length)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.PixelCollection_SetArea(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, values, (UIntPtr)length, out exception);
        else
          NativeMethods.X86.PixelCollection_SetArea(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, values, (UIntPtr)length, out exception);
        CheckException(exception);
      }
      public IntPtr ToByteArray(int x, int y, int width, int height, string mapping)
      {
        using (INativeInstance mappingNative = UTF8Marshaler.CreateInstance(mapping))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PixelCollection_ToByteArray(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, mappingNative.Instance, out exception);
          else
            result = NativeMethods.X86.PixelCollection_ToByteArray(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, mappingNative.Instance, out exception);
          MagickException magickException = MagickExceptionHelper.Create(exception);
          if (MagickExceptionHelper.IsError(magickException))
          {
            if (result != IntPtr.Zero)
              MagickMemory.Relinquish(result);
            throw magickException;
          }
          RaiseWarning(magickException);
          return result;
        }
      }
      public IntPtr ToShortArray(int x, int y, int width, int height, string mapping)
      {
        using (INativeInstance mappingNative = UTF8Marshaler.CreateInstance(mapping))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PixelCollection_ToShortArray(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, mappingNative.Instance, out exception);
          else
            result = NativeMethods.X86.PixelCollection_ToShortArray(Instance, (UIntPtr)x, (UIntPtr)y, (UIntPtr)width, (UIntPtr)height, mappingNative.Instance, out exception);
          MagickException magickException = MagickExceptionHelper.Create(exception);
          if (MagickExceptionHelper.IsError(magickException))
          {
            if (result != IntPtr.Zero)
              MagickMemory.Relinquish(result);
            throw magickException;
          }
          RaiseWarning(magickException);
          return result;
        }
      }
    }
    internal static IntPtr GetInstance(PixelCollection instance)
    {
      if (instance == null)
        return IntPtr.Zero;
      return instance._NativeInstance.Instance;
    }
  }
}
