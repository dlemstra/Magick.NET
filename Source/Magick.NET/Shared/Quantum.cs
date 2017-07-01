// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;

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
    /// <summary>
    /// Class that can be used to acquire information about the Quantum.
    /// </summary>
    public static partial class Quantum
    {
        /// <summary>
        /// Gets the Quantum depth.
        /// </summary>
        public static int Depth
        {
            get
            {
                return NativeQuantum.Depth;
            }
        }

        /// <summary>
        /// Gets the maximum value of the quantum.
        /// </summary>
        public static QuantumType Max
        {
            get
            {
                return NativeQuantum.Max;
            }
        }

        internal static QuantumType Convert(byte value)
        {
#if Q16 || Q16HDRI
            return (QuantumType)(257UL * value);
#else
            return value;
#endif
        }

        internal static QuantumType Convert(double value)
        {
            if (value < 0)
                return 0;
            if (value > Max)
                return Max;

            return (QuantumType)value;
        }

        internal static QuantumType Convert(int value)
        {
            if (value < 0)
                return 0;
            if (value > Max)
                return Max;

            return (QuantumType)value;
        }

#if !Q16
        internal static QuantumType Convert(ushort value)
        {
#if Q8
            return (QuantumType)((value + 128U) / 257U);
#elif Q16HDRI
            return (QuantumType)value;
#endif
        }
#endif

        internal static QuantumType ScaleToQuantum(double value)
        {
            return (QuantumType)Math.Min(Math.Max(0, value * Max), Max);
        }

        internal static byte ScaleToByte(QuantumType value)
        {
            return NativeQuantum.ScaleToByte(value);
        }

        internal static double ScaleToDouble(QuantumType value)
        {
            return (1.0 / Max) * value;
        }
    }
}