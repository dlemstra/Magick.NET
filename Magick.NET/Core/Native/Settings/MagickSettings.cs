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
  public partial class MagickSettings : IDisposable
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Create();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Adjoin_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Adjoin_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_AlphaColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_AlphaColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_BackgroundColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_BackgroundColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_BorderColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_BorderColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_ColorSpace_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_ColorSpace_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_ColorType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_ColorType_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_CompressionMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_CompressionMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Debug_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Debug_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Density_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Density_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_Endian_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Endian_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_FileName_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_FileName_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Format_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Format_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Font_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Font_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickSettings_FontPointsize_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_FontPointsize_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Page_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Page_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Ping_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Ping_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Verbose_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Verbose_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Clone(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_GetOption(IntPtr Instance, IntPtr key);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_RemoveOption(IntPtr Instance, IntPtr key);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetColorFuzz(IntPtr Instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetInterlace(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetMonochrome(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetNumberScenes(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetOption(IntPtr Instance, IntPtr key, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetQuality(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetScenes(IntPtr Instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetScene(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetSize(IntPtr Instance, IntPtr value);
      }
      public static class X86
      {
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Create();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Adjoin_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Adjoin_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_AlphaColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_AlphaColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_BackgroundColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_BackgroundColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_BorderColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_BorderColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_ColorSpace_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_ColorSpace_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_ColorType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_ColorType_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_CompressionMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_CompressionMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Debug_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Debug_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Density_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Density_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickSettings_Endian_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Endian_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_FileName_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_FileName_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Format_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Format_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Font_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Font_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickSettings_FontPointsize_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_FontPointsize_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Page_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Page_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Ping_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Ping_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickSettings_Verbose_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_Verbose_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_Clone(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_GetOption(IntPtr Instance, IntPtr key);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickSettings_RemoveOption(IntPtr Instance, IntPtr key);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetColorFuzz(IntPtr Instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetInterlace(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetMonochrome(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetNumberScenes(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetOption(IntPtr Instance, IntPtr key, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetQuality(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetScenes(IntPtr Instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetScene(IntPtr Instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickSettings_SetSize(IntPtr Instance, IntPtr value);
      }
    }
    private NativeMagickSettings _NativeInstance;
    private sealed class NativeMagickSettings : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_Dispose(instance);
        else
          NativeMethods.X86.MagickSettings_Dispose(instance);
      }
      public NativeMagickSettings()
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.MagickSettings_Create();
        else
          _Instance = NativeMethods.X86.MagickSettings_Create();
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativeMagickSettings(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(MagickSettings).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public bool Adjoin
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Adjoin_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Adjoin_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_Adjoin_Set(Instance, value);
          else
            NativeMethods.X86.MagickSettings_Adjoin_Set(Instance, value);
        }
      }
      public MagickColor AlphaColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_AlphaColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_AlphaColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueNative = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_AlphaColor_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_AlphaColor_Set(Instance, valueNative.Instance);
          }
        }
      }
      public MagickColor BackgroundColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_BackgroundColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_BackgroundColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueNative = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_BackgroundColor_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_BackgroundColor_Set(Instance, valueNative.Instance);
          }
        }
      }
      public MagickColor BorderColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_BorderColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_BorderColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueNative = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_BorderColor_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_BorderColor_Set(Instance, valueNative.Instance);
          }
        }
      }
      public ColorSpace ColorSpace
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_ColorSpace_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_ColorSpace_Get(Instance);
          return (ColorSpace)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_ColorSpace_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickSettings_ColorSpace_Set(Instance, (UIntPtr)value);
        }
      }
      public ColorType ColorType
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_ColorType_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_ColorType_Get(Instance);
          return (ColorType)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_ColorType_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickSettings_ColorType_Set(Instance, (UIntPtr)value);
        }
      }
      public CompressionMethod CompressionMethod
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_CompressionMethod_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_CompressionMethod_Get(Instance);
          return (CompressionMethod)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_CompressionMethod_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickSettings_CompressionMethod_Set(Instance, (UIntPtr)value);
        }
      }
      public bool Debug
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Debug_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Debug_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_Debug_Set(Instance, value);
          else
            NativeMethods.X86.MagickSettings_Debug_Set(Instance, value);
        }
      }
      public string Density
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Density_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Density_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
        set
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_Density_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_Density_Set(Instance, valueNative.Instance);
          }
        }
      }
      public Endian Endian
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Endian_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Endian_Get(Instance);
          return (Endian)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_Endian_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickSettings_Endian_Set(Instance, (UIntPtr)value);
        }
      }
      public string FileName
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_FileName_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_FileName_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
        set
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_FileName_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_FileName_Set(Instance, valueNative.Instance);
          }
        }
      }
      public string Format
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Format_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Format_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
        set
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_Format_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_Format_Set(Instance, valueNative.Instance);
          }
        }
      }
      public string Font
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Font_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Font_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
        set
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_Font_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_Font_Set(Instance, valueNative.Instance);
          }
        }
      }
      public double FontPointsize
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_FontPointsize_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_FontPointsize_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_FontPointsize_Set(Instance, value);
          else
            NativeMethods.X86.MagickSettings_FontPointsize_Set(Instance, value);
        }
      }
      public string Page
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Page_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Page_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
        set
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_Page_Set(Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_Page_Set(Instance, valueNative.Instance);
          }
        }
      }
      public bool Ping
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Ping_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Ping_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_Ping_Set(Instance, value);
          else
            NativeMethods.X86.MagickSettings_Ping_Set(Instance, value);
        }
      }
      public bool Verbose
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickSettings_Verbose_Get(Instance);
          else
            result = NativeMethods.X86.MagickSettings_Verbose_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_Verbose_Set(Instance, value);
          else
            NativeMethods.X86.MagickSettings_Verbose_Set(Instance, value);
        }
      }
      public IntPtr Clone()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickSettings_Clone(Instance, out exception);
        else
          result = NativeMethods.X86.MagickSettings_Clone(Instance, out exception);
        CheckException(exception, result);
        return result;
      }
      public string GetOption(string key)
      {
        using (INativeInstance keyNative = UTF8Marshaler.CreateInstance(key))
        {
          if (NativeLibrary.Is64Bit)
            return UTF8Marshaler.NativeToManaged(NativeMethods.X64.MagickSettings_GetOption(Instance, keyNative.Instance));
          else
            return UTF8Marshaler.NativeToManaged(NativeMethods.X86.MagickSettings_GetOption(Instance, keyNative.Instance));
        }
      }
      public string RemoveOption(string key)
      {
        using (INativeInstance keyNative = UTF8Marshaler.CreateInstance(key))
        {
          if (NativeLibrary.Is64Bit)
            return UTF8Marshaler.NativeToManaged(NativeMethods.X64.MagickSettings_RemoveOption(Instance, keyNative.Instance));
          else
            return UTF8Marshaler.NativeToManaged(NativeMethods.X86.MagickSettings_RemoveOption(Instance, keyNative.Instance));
        }
      }
      public void SetColorFuzz(double value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetColorFuzz(Instance, value);
        else
          NativeMethods.X86.MagickSettings_SetColorFuzz(Instance, value);
      }
      public void SetInterlace(Interlace value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetInterlace(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.MagickSettings_SetInterlace(Instance, (UIntPtr)value);
      }
      public void SetMonochrome(bool value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetMonochrome(Instance, value);
        else
          NativeMethods.X86.MagickSettings_SetMonochrome(Instance, value);
      }
      public void SetNumberScenes(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetNumberScenes(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.MagickSettings_SetNumberScenes(Instance, (UIntPtr)value);
      }
      public void SetOption(string key, string value)
      {
        using (INativeInstance keyNative = UTF8Marshaler.CreateInstance(key))
        {
          using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickSettings_SetOption(Instance, keyNative.Instance, valueNative.Instance);
            else
              NativeMethods.X86.MagickSettings_SetOption(Instance, keyNative.Instance, valueNative.Instance);
          }
        }
      }
      public void SetQuality(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetQuality(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.MagickSettings_SetQuality(Instance, (UIntPtr)value);
      }
      public void SetScenes(string value)
      {
        using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_SetScenes(Instance, valueNative.Instance);
          else
            NativeMethods.X86.MagickSettings_SetScenes(Instance, valueNative.Instance);
        }
      }
      public void SetScene(int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickSettings_SetScene(Instance, (UIntPtr)value);
        else
          NativeMethods.X86.MagickSettings_SetScene(Instance, (UIntPtr)value);
      }
      public void SetSize(string value)
      {
        using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickSettings_SetSize(Instance, valueNative.Instance);
          else
            NativeMethods.X86.MagickSettings_SetSize(Instance, valueNative.Instance);
        }
      }
    }
    internal static IntPtr GetInstance(MagickSettings instance)
    {
      if (instance == null)
        return IntPtr.Zero;
      return instance._NativeInstance.Instance;
    }
  }
}
