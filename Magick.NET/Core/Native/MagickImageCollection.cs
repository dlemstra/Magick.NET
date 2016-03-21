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
  public partial class MagickImageCollection
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.MagickImageCollection+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Append(IntPtr image, [MarshalAs(UnmanagedType.Bool)] bool stack, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Coalesce(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Combine(IntPtr image, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Deconstruct(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Dispose(IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Evaluate(IntPtr image, UIntPtr evaluateOperator, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Flatten(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Map(IntPtr image, IntPtr settings, IntPtr remapImage, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Merge(IntPtr image, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Montage(IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Morph(IntPtr image, UIntPtr frames, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Optimize(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_OptimizePlus(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_OptimizeTransparency(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Quantize(IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_ReadBlob(IntPtr settings, byte[] data, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_ReadFile(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Smush(IntPtr image, IntPtr offset, [MarshalAs(UnmanagedType.Bool)] bool stack, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_WriteBlob(IntPtr image, IntPtr settings, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_WriteFile(IntPtr image, IntPtr settings, out IntPtr exception);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Append(IntPtr image, [MarshalAs(UnmanagedType.Bool)] bool stack, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Coalesce(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Combine(IntPtr image, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Deconstruct(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Dispose(IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Evaluate(IntPtr image, UIntPtr evaluateOperator, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Flatten(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Map(IntPtr image, IntPtr settings, IntPtr remapImage, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Merge(IntPtr image, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Montage(IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Morph(IntPtr image, UIntPtr frames, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Optimize(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_OptimizePlus(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_OptimizeTransparency(IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_Quantize(IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_ReadBlob(IntPtr settings, byte[] data, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_ReadFile(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_Smush(IntPtr image, IntPtr offset, [MarshalAs(UnmanagedType.Bool)] bool stack, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImageCollection_WriteBlob(IntPtr image, IntPtr settings, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImageCollection_WriteFile(IntPtr image, IntPtr settings, out IntPtr exception);
      }
    }
    private sealed class NativeMagickImageCollection : NativeHelper
    {
      public IntPtr Append(MagickImage image, bool stack)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Append(MagickImage.GetInstance(image), stack, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Append(MagickImage.GetInstance(image), stack, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Coalesce(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Coalesce(MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Coalesce(MagickImage.GetInstance(image), out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Combine(MagickImage image, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Combine(MagickImage.GetInstance(image), (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Combine(MagickImage.GetInstance(image), (UIntPtr)channels, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Deconstruct(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Deconstruct(MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Deconstruct(MagickImage.GetInstance(image), out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public static void Dispose(IntPtr value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImageCollection_Dispose(value);
        else
          NativeMethods.X86.MagickImageCollection_Dispose(value);
      }
      public IntPtr Evaluate(MagickImage image, EvaluateOperator evaluateOperator)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Evaluate(MagickImage.GetInstance(image), (UIntPtr)evaluateOperator, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Evaluate(MagickImage.GetInstance(image), (UIntPtr)evaluateOperator, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Flatten(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Flatten(MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Flatten(MagickImage.GetInstance(image), out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void Map(MagickImage image, QuantizeSettings settings, MagickImage remapImage)
      {
        using (INativeInstance settingsNative = QuantizeSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImageCollection_Map(MagickImage.GetInstance(image), settingsNative.Instance, MagickImage.GetInstance(remapImage), out exception);
          else
            NativeMethods.X86.MagickImageCollection_Map(MagickImage.GetInstance(image), settingsNative.Instance, MagickImage.GetInstance(remapImage), out exception);
          CheckException(exception);
        }
      }
      public IntPtr Merge(MagickImage image, LayerMethod method)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Merge(MagickImage.GetInstance(image), (UIntPtr)method, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Merge(MagickImage.GetInstance(image), (UIntPtr)method, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Montage(MagickImage image, MontageSettings settings)
      {
        using (INativeInstance settingsNative = MontageSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImageCollection_Montage(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImageCollection_Montage(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          MagickException magickException = MagickExceptionHelper.Create(exception);
          if (MagickExceptionHelper.IsError(magickException))
          {
            if (result != IntPtr.Zero)
              Dispose(result);
            throw magickException;
          }
          RaiseWarning(magickException);
          return result;
        }
      }
      public IntPtr Morph(MagickImage image, int frames)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Morph(MagickImage.GetInstance(image), (UIntPtr)frames, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Morph(MagickImage.GetInstance(image), (UIntPtr)frames, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr Optimize(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Optimize(MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Optimize(MagickImage.GetInstance(image), out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr OptimizePlus(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_OptimizePlus(MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_OptimizePlus(MagickImage.GetInstance(image), out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void OptimizeTransparency(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImageCollection_OptimizeTransparency(MagickImage.GetInstance(image), out exception);
        else
          NativeMethods.X86.MagickImageCollection_OptimizeTransparency(MagickImage.GetInstance(image), out exception);
        CheckException(exception);
      }
      public void Quantize(MagickImage image, QuantizeSettings settings)
      {
        using (INativeInstance settingsNative = QuantizeSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImageCollection_Quantize(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          else
            NativeMethods.X86.MagickImageCollection_Quantize(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          CheckException(exception);
        }
      }
      public IntPtr ReadBlob(MagickSettings settings, byte[] data, int length)
      {
        using (INativeInstance settingsNative = MagickSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImageCollection_ReadBlob(settingsNative.Instance, data, (UIntPtr)length, out exception);
          else
            result = NativeMethods.X86.MagickImageCollection_ReadBlob(settingsNative.Instance, data, (UIntPtr)length, out exception);
          MagickException magickException = MagickExceptionHelper.Create(exception);
          if (MagickExceptionHelper.IsError(magickException))
          {
            if (result != IntPtr.Zero)
              Dispose(result);
            throw magickException;
          }
          RaiseWarning(magickException);
          return result;
        }
      }
      public IntPtr ReadFile(MagickSettings settings)
      {
        using (INativeInstance settingsNative = MagickSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImageCollection_ReadFile(settingsNative.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImageCollection_ReadFile(settingsNative.Instance, out exception);
          MagickException magickException = MagickExceptionHelper.Create(exception);
          if (MagickExceptionHelper.IsError(magickException))
          {
            if (result != IntPtr.Zero)
              Dispose(result);
            throw magickException;
          }
          RaiseWarning(magickException);
          return result;
        }
      }
      public IntPtr Smush(MagickImage image, int offset, bool stack)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImageCollection_Smush(MagickImage.GetInstance(image), (IntPtr)offset, stack, out exception);
        else
          result = NativeMethods.X86.MagickImageCollection_Smush(MagickImage.GetInstance(image), (IntPtr)offset, stack, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public IntPtr WriteBlob(MagickImage image, MagickSettings settings, out UIntPtr length)
      {
        using (INativeInstance settingsNative = MagickSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImageCollection_WriteBlob(MagickImage.GetInstance(image), settingsNative.Instance, out length, out exception);
          else
            result = NativeMethods.X86.MagickImageCollection_WriteBlob(MagickImage.GetInstance(image), settingsNative.Instance, out length, out exception);
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
      public void WriteFile(MagickImage image, MagickSettings settings)
      {
        using (INativeInstance settingsNative = MagickSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImageCollection_WriteFile(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          else
            NativeMethods.X86.MagickImageCollection_WriteFile(MagickImage.GetInstance(image), settingsNative.Instance, out exception);
          CheckException(exception);
        }
      }
    }
  }
}
