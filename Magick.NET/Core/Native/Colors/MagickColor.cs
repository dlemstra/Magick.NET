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
  public partial class MagickColor
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.MagickColor+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickColor_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong MagickColor_Count_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Red_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Red_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Green_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Green_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Blue_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Blue_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Alpha_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Alpha_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Black_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Black_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickColor_FuzzyEquals(IntPtr Instance, IntPtr other, QuantumType fuzz);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickColor_Initialize(IntPtr Instance, IntPtr value);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickColor_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong MagickColor_Count_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Red_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Red_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Green_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Green_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Blue_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Blue_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Alpha_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Alpha_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType MagickColor_Black_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickColor_Black_Set(IntPtr instance, QuantumType value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickColor_FuzzyEquals(IntPtr Instance, IntPtr other, QuantumType fuzz);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickColor_Initialize(IntPtr Instance, IntPtr value);
      }
    }
    private sealed class NativeMagickColor : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickColor_Dispose(instance);
        else
          NativeMethods.X86.MagickColor_Dispose(instance);
      }
      public NativeMagickColor()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.MagickColor_Create();
        else
          _Instance = NativeMethods.X86.MagickColor_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativeMagickColor(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(MagickColor).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public ulong Count
      {
        get
        {
          ulong result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Count_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Count_Get(Instance);
          return result;
        }
      }
      public QuantumType Red
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Red_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Red_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickColor_Red_Set(Instance, value);
          else
            NativeMethods.X86.MagickColor_Red_Set(Instance, value);
        }
      }
      public QuantumType Green
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Green_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Green_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickColor_Green_Set(Instance, value);
          else
            NativeMethods.X86.MagickColor_Green_Set(Instance, value);
        }
      }
      public QuantumType Blue
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Blue_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Blue_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickColor_Blue_Set(Instance, value);
          else
            NativeMethods.X86.MagickColor_Blue_Set(Instance, value);
        }
      }
      public QuantumType Alpha
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Alpha_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Alpha_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickColor_Alpha_Set(Instance, value);
          else
            NativeMethods.X86.MagickColor_Alpha_Set(Instance, value);
        }
      }
      public QuantumType Black
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickColor_Black_Get(Instance);
          else
            result = NativeMethods.X86.MagickColor_Black_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickColor_Black_Set(Instance, value);
          else
            NativeMethods.X86.MagickColor_Black_Set(Instance, value);
        }
      }
      public bool FuzzyEquals(MagickColor other, QuantumType fuzz)
      {
        using (INativeInstance otherNative = MagickColor.CreateInstance(other))
        {
          if (NativeLibrary.Is64Bit)
            return NativeMethods.X64.MagickColor_FuzzyEquals(Instance, otherNative.Instance, fuzz);
          else
            return NativeMethods.X86.MagickColor_FuzzyEquals(Instance, otherNative.Instance, fuzz);
        }
      }
      public bool Initialize(string value)
      {
        using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
        {
          if (NativeLibrary.Is64Bit)
            return NativeMethods.X64.MagickColor_Initialize(Instance, valueNative.Instance);
          else
            return NativeMethods.X86.MagickColor_Initialize(Instance, valueNative.Instance);
        }
      }
    }
    internal static INativeInstance CreateInstance(MagickColor instance)
    {
      if (instance == null)
        return NativeInstance.Zero;
      return instance.CreateNativeInstance();
    }
    internal static MagickColor CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;
      using (NativeMagickColor nativeInstance = new NativeMagickColor(instance))
      {
        return new MagickColor(nativeInstance);
      }
    }
  }
}
