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
  internal partial class PointInfo
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PointInfo_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PointInfo_Y_Get(IntPtr instance);
      }
      public static class X86
      {
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PointInfo_X_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double PointInfo_Y_Get(IntPtr instance);
      }
    }
    private sealed class NativePointInfo : ConstNativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      public NativePointInfo(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(PointInfo).ToString());
          return _Instance;
        }
        set
        {
          _Instance = value;
        }
      }
      public double X
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PointInfo_X_Get(Instance);
          else
            result = NativeMethods.X86.PointInfo_X_Get(Instance);
          return result;
        }
      }
      public double Y
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.PointInfo_Y_Get(Instance);
          else
            result = NativeMethods.X86.PointInfo_Y_Get(Instance);
          return result;
        }
      }
    }
  }
}
