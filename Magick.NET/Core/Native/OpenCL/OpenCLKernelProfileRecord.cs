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
  public partial class OpenCLKernelProfileRecord
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCLKernelProfileRecord+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_Count_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_MaximumDuration_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_MinimumDuration_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLKernelProfileRecord_Name_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_TotalDuration_Get(IntPtr instance);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.OpenCLKernelProfileRecord+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_Count_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_MaximumDuration_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_MinimumDuration_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OpenCLKernelProfileRecord_Name_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long OpenCLKernelProfileRecord_TotalDuration_Get(IntPtr instance);
      }
    }
    private sealed class NativeOpenCLKernelProfileRecord : ConstNativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(OpenCLKernelProfileRecord).ToString());
          return _Instance;
        }
        set
        {
          _Instance = value;
        }
      }
      public long Count
      {
        get
        {
          long result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLKernelProfileRecord_Count_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLKernelProfileRecord_Count_Get(Instance);
          return result;
        }
      }
      public long MaximumDuration
      {
        get
        {
          long result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLKernelProfileRecord_MaximumDuration_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLKernelProfileRecord_MaximumDuration_Get(Instance);
          return result;
        }
      }
      public long MinimumDuration
      {
        get
        {
          long result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLKernelProfileRecord_MinimumDuration_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLKernelProfileRecord_MinimumDuration_Get(Instance);
          return result;
        }
      }
      public string Name
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLKernelProfileRecord_Name_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLKernelProfileRecord_Name_Get(Instance);
          return UTF8Marshaler.NativeToManaged(result);
        }
      }
      public long TotalDuration
      {
        get
        {
          long result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.OpenCLKernelProfileRecord_TotalDuration_Get(Instance);
          else
            result = NativeMethods.X86.OpenCLKernelProfileRecord_TotalDuration_Get(Instance);
          return result;
        }
      }
    }
  }
}
