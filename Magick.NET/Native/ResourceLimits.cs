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
  public static partial class ResourceLimits
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Disk_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Disk_Set(ulong value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Height_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Height_Set(ulong value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Memory_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Memory_Set(ulong value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Thread_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Thread_Set(ulong value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Throttle_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Throttle_Set(ulong value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Width_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Width_Set(ulong value);
      }
      public static class X86
      {
        static X86() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Disk_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Disk_Set(ulong value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Height_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Height_Set(ulong value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Memory_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Memory_Set(ulong value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Thread_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Thread_Set(ulong value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Throttle_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Throttle_Set(ulong value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong ResourceLimits_Width_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResourceLimits_Width_Set(ulong value);
      }
      public static ulong Disk
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Disk_Get();
          else
            return X86.ResourceLimits_Disk_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Disk_Set(value);
          else
            X86.ResourceLimits_Disk_Set(value);
        }
      }
      public static ulong Height
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Height_Get();
          else
            return X86.ResourceLimits_Height_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Height_Set(value);
          else
            X86.ResourceLimits_Height_Set(value);
        }
      }
      public static ulong Memory
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Memory_Get();
          else
            return X86.ResourceLimits_Memory_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Memory_Set(value);
          else
            X86.ResourceLimits_Memory_Set(value);
        }
      }
      public static ulong Thread
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Thread_Get();
          else
            return X86.ResourceLimits_Thread_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Thread_Set(value);
          else
            X86.ResourceLimits_Thread_Set(value);
        }
      }
      public static ulong Throttle
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Throttle_Get();
          else
            return X86.ResourceLimits_Throttle_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Throttle_Set(value);
          else
            X86.ResourceLimits_Throttle_Set(value);
        }
      }
      public static ulong Width
      {
        get
        {
          if (NativeLibrary.Is64Bit)
            return X64.ResourceLimits_Width_Get();
          else
            return X86.ResourceLimits_Width_Get();
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            X64.ResourceLimits_Width_Set(value);
          else
            X86.ResourceLimits_Width_Set(value);
        }
      }
    }
  }
}
