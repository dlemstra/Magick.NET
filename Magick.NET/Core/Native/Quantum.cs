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
  public static partial class Quantum
  {
    private static class NativeMethods
    {
      [SuppressMessage("Microsoft.Performance", "CA1810: InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "ImageMagick.Quantum+NativeMethods.#.cctor()")]
      static NativeMethods() { NativeLibraryLoader.Load(); }
      public static class X64
      {
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr Quantum_Depth_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType Quantum_Max_Get();
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte Quantum_ScaleToByte(QuantumType value);
      }
      public static class X86
      {
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr Quantum_Depth_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern QuantumType Quantum_Max_Get();
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte Quantum_ScaleToByte(QuantumType value);
      }
    }
    private static class NativeQuantum
    {
      public static int Depth
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.Quantum_Depth_Get();
          else
            result = NativeMethods.X86.Quantum_Depth_Get();
          return (int)result;
        }
      }
      public static QuantumType Max
      {
        get
        {
          QuantumType result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.Quantum_Max_Get();
          else
            result = NativeMethods.X86.Quantum_Max_Get();
          return result;
        }
      }
      public static byte ScaleToByte(QuantumType value)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.Quantum_ScaleToByte(value);
        else
          return NativeMethods.X86.Quantum_ScaleToByte(value);
      }
    }
  }
}
