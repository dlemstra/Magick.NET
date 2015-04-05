//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
	///=============================================================================================
	///<summary>
	/// Class that can be used to acquire information about the Quantum.
	///</summary>
	public static class Quantum
	{
		//===========================================================================================
		internal static QuantumType Scale(double value)
		{
			return (QuantumType)(value / Max);
		}
		//===========================================================================================
		internal static double Scale(QuantumType value)
		{
			return ((double)1.0 / (double)Max) * value;
		}
		//===========================================================================================
		internal static QuantumType Convert(Byte value)
		{
			return Wrapper.Quantum.Convert(value);
		}
		///==========================================================================================
		///<summary>
		/// Returns the Quantum depth.
		///</summary>
		public static int Depth
		{
			get
			{
				return Wrapper.Quantum.Depth;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the maximum value of the quantum.
		///</summary>
#if Q16
		[CLSCompliant(false)]
#endif
		public static QuantumType Max
		{
			get
			{
				return Wrapper.Quantum.Max;
			}
		}
		//===========================================================================================
	}
}