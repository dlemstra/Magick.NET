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
  internal partial class PointInfoCollection : IDisposable
  {
    private static class NativeMethods
    {
      public static class X64
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PointInfoCollection+NativeMethods.X64#.cctor()")]
        static X64() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PointInfoCollection_Create(UIntPtr length);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PointInfoCollection_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PointInfoCollection_Set(IntPtr Instance, UIntPtr index, double x, double y);
      }
      public static class X86
      {
        [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.PointInfoCollection+NativeMethods.X86#.cctor()")]
        static X86() { NativeLibraryLoader.Load(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PointInfoCollection_Create(UIntPtr length);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PointInfoCollection_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PointInfoCollection_Set(IntPtr Instance, UIntPtr index, double x, double y);
      }
    }
    private NativePointInfoCollection _NativeInstance;
    private sealed class NativePointInfoCollection : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.PointInfoCollection_Dispose(instance);
        else
          NativeMethods.X86.PointInfoCollection_Dispose(instance);
      }
      public NativePointInfoCollection(int length)
      {
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.PointInfoCollection_Create((UIntPtr)length);
        else
          _Instance = NativeMethods.X86.PointInfoCollection_Create((UIntPtr)length);
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(PointInfoCollection).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public void Set(int index, double x, double y)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.PointInfoCollection_Set(Instance, (UIntPtr)index, x, y);
        else
          NativeMethods.X86.PointInfoCollection_Set(Instance, (UIntPtr)index, x, y);
      }
    }
    internal static IntPtr GetInstance(PointInfoCollection instance)
    {
      if (instance == null)
        return IntPtr.Zero;
      return instance._NativeInstance.Instance;
    }
  }
}
