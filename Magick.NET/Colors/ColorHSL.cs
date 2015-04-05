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
	/// Class that represents a HSL color.
	///</summary>
	public sealed class ColorHSL : ColorBase
	{
		//===========================================================================================
		private ColorHSL(MagickColor color)
			: base(color)
		{
			Tuple<double, double, double> value = Wrapper.MagickColor.ConvertRGBToHSL(MagickColor.GetInstance(color));

			Hue = value.Item1;
			Saturation = value.Item3;
			Luminosity = value.Item2;
		}
		///==========================================================================================
		/// <summary>
		/// Updates the color value in an inherited class.
		/// </summary>
		protected override void UpdateValue()
		{
			Tuple<double, double, double> value = new Tuple<double, double, double>(Hue, Saturation, Luminosity);

			Wrapper.MagickColor color = Wrapper.MagickColor.ConvertHSLToRGB(value);
			Value.R = color.R;
			Value.G = color.G;
			Value.B = color.B;
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorHSL class.
		///</summary>
		///<param name="hue">Hue component value of this color.</param>
		///<param name="saturation">Saturation component value of this color.</param>
		///<param name="luminosity">Luminosity component value of this color.</param>
		public ColorHSL(double hue, double saturation, double luminosity)
			: base(new MagickColor(0, 0, 0))
		{
			Hue = hue;
			Saturation = saturation;
			Luminosity = luminosity;
		}
		///==========================================================================================
		///<summary>
		/// Hue component value of this color.
		///</summary>
		public double Hue
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Luminosity component value of this color.
		///</summary>
		public double Luminosity
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Saturation component value of this color.
		///</summary>
		public double Saturation
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		public static implicit operator ColorHSL(MagickColor color)
		{
			return FromMagickColor(color);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		public static ColorHSL FromMagickColor(MagickColor color)
		{
			if (color == null)
				return null;

			return new ColorHSL(color);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
