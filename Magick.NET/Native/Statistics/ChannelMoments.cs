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
  public partial class ChannelMoments
  {
    private static class NativeMethods
    {
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ChannelMoments_Centroid_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseAngle_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ChannelMoments_EllipseAxis_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseEccentricity_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseIntensity_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_GetHuInvariants(IntPtr Instance, UIntPtr index);
      }
      public static class X86
      {
        static X86() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ChannelMoments_Centroid_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseAngle_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ChannelMoments_EllipseAxis_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseEccentricity_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_EllipseIntensity_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelMoments_GetHuInvariants(IntPtr Instance, UIntPtr index);
      }
    }
    private sealed class NativeChannelMoments : ConstNativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      public NativeChannelMoments(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(ChannelMoments).ToString());
          return _Instance;
        }
        set
        {
          _Instance = value;
        }
      }
      public PointInfo Centroid
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.ChannelMoments_Centroid_Get(Instance);
          else
            result = NativeMethods.X86.ChannelMoments_Centroid_Get(Instance);
          return PointInfo.CreateInstance(result);
        }
      }
      public double EllipseAngle
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.ChannelMoments_EllipseAngle_Get(Instance);
          else
            result = NativeMethods.X86.ChannelMoments_EllipseAngle_Get(Instance);
          return result;
        }
      }
      public PointInfo EllipseAxis
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.ChannelMoments_EllipseAxis_Get(Instance);
          else
            result = NativeMethods.X86.ChannelMoments_EllipseAxis_Get(Instance);
          return PointInfo.CreateInstance(result);
        }
      }
      public double EllipseEccentricity
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.ChannelMoments_EllipseEccentricity_Get(Instance);
          else
            result = NativeMethods.X86.ChannelMoments_EllipseEccentricity_Get(Instance);
          return result;
        }
      }
      public double EllipseIntensity
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.ChannelMoments_EllipseIntensity_Get(Instance);
          else
            result = NativeMethods.X86.ChannelMoments_EllipseIntensity_Get(Instance);
          return result;
        }
      }
      public double GetHuInvariants(int index)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.ChannelMoments_GetHuInvariants(Instance, (UIntPtr)index);
        else
          return NativeMethods.X86.ChannelMoments_GetHuInvariants(Instance, (UIntPtr)index);
      }
    }
  }
}
