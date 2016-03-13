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
  public partial class ChannelPerceptualHash
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.ChannelPerceptualHash+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelPerceptualHash_GetSrgbHuPhash(IntPtr Instance, UIntPtr index);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelPerceptualHash_GetHclpHuPhash(IntPtr Instance, UIntPtr index);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelPerceptualHash_GetSrgbHuPhash(IntPtr Instance, UIntPtr index);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ChannelPerceptualHash_GetHclpHuPhash(IntPtr Instance, UIntPtr index);
      }
    }
    private sealed class NativeChannelPerceptualHash : ConstNativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      public NativeChannelPerceptualHash(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(ChannelPerceptualHash).ToString());
          return _Instance;
        }
        set
        {
          _Instance = value;
        }
      }
      public double GetSrgbHuPhash(int index)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.ChannelPerceptualHash_GetSrgbHuPhash(Instance, (UIntPtr)index);
        else
          return NativeMethods.X86.ChannelPerceptualHash_GetSrgbHuPhash(Instance, (UIntPtr)index);
      }
      public double GetHclpHuPhash(int index)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.ChannelPerceptualHash_GetHclpHuPhash(Instance, (UIntPtr)index);
        else
          return NativeMethods.X86.ChannelPerceptualHash_GetHclpHuPhash(Instance, (UIntPtr)index);
      }
    }
  }
}
