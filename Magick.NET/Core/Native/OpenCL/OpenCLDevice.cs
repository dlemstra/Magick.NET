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
  public partial class OpenCLDevice
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCLDevice+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr OpenCLDevice_DeviceType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenCLDevice_IsEnabled_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenCLDevice_IsEnabled_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_Name_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_Version_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_GetKernelProfileRecords(IntPtr Instance, out UIntPtr length);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_GetKernelProfileRecord(IntPtr list, UIntPtr index);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenCLDevice_SetProfileKernels(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCLDevice+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr OpenCLDevice_DeviceType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenCLDevice_IsEnabled_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenCLDevice_IsEnabled_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_Name_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_Version_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_GetKernelProfileRecords(IntPtr Instance, out UIntPtr length);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLDevice_GetKernelProfileRecord(IntPtr list, UIntPtr index);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenCLDevice_SetProfileKernels(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool value);
      }
    }
    private sealed class NativeOpenCLDevice : ConstNativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(OpenCLDevice).ToString());
          return _Instance;
        }
        set
        {
          _Instance = value;
        }
      }
      public OpenCLDeviceType DeviceType
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLDevice_DeviceType_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLDevice_DeviceType_Get(Instance);
          return (OpenCLDeviceType)result;
        }
      }
      public bool IsEnabled
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLDevice_IsEnabled_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLDevice_IsEnabled_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.OpenCLDevice_IsEnabled_Set(Instance, value);
          else
            NativeMethods.X86.OpenCLDevice_IsEnabled_Set(Instance, value);
        }
      }
      public string Name
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLDevice_Name_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLDevice_Name_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
      }
      public string Version
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLDevice_Version_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLDevice_Version_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
      }
      public IntPtr GetKernelProfileRecords(out UIntPtr length)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.OpenCLDevice_GetKernelProfileRecords(Instance, out length);
        else
          return NativeMethods.X86.OpenCLDevice_GetKernelProfileRecords(Instance, out length);
      }
      public static IntPtr GetKernelProfileRecord(IntPtr list, int index)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.OpenCLDevice_GetKernelProfileRecord(list, (UIntPtr)index);
        else
          return NativeMethods.X86.OpenCLDevice_GetKernelProfileRecord(list, (UIntPtr)index);
      }
      public void SetProfileKernels(bool value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.OpenCLDevice_SetProfileKernels(Instance, value);
        else
          NativeMethods.X86.OpenCLDevice_SetProfileKernels(Instance, value);
      }
    }
  }
}
