//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

using namespace System::Drawing;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that represents a color.
	///</summary>
	public ref class MagickColor sealed : IEquatable<MagickColor^>, IComparable<MagickColor^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(unsigned char red, unsigned char green, unsigned char blue, unsigned char alpha);
		//===========================================================================================
		void ParseColor(String^ color);
		//===========================================================================================
		static Magick::Quantum ParseHex(String^ color, int offset, int length);
		//===========================================================================================
		void ParseQ8HexColor(String^ color);
		//===========================================================================================
#if (MAGICKCORE_QUANTUM_DEPTH > 8)
		void ParseQ16HexColor(String^ color);
#endif
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickColor(MagickColor^ color);
		//===========================================================================================
		MagickColor(Magick::Color color);
		//===========================================================================================
		property Magick::Quantum Opacity
		{
			Magick::Quantum get();
		}
		//===========================================================================================
		const Magick::Color* CreateColor();
		//===========================================================================================
		void Initialize(Color color);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class.
		///</summary>
		MagickColor();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class using the specified color.
		///</summary>
		///<param name="color">The color to use.</param>
		MagickColor(Color color);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class.
		///</summary>
		///<param name="red">Red component value of this color.</param>
		///<param name="green">Green component value of this color.</param>
		///<param name="blue">Blue component value of this color.</param>
		QUANTUM_CLS_COMPLIANT MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class.
		///</summary>
		///<param name="red">Red component value of this color.</param>
		///<param name="green">Green component value of this color.</param>
		///<param name="blue">Blue component value of this color.</param>
		///<param name="alpha">Alpha component value of this color.</param>
		QUANTUM_CLS_COMPLIANT MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue,
			Magick::Quantum alpha);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class using the specified RGBA hex string or
		/// name of the color (http://www.imagemagick.org/script/color.php).
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		/// For example: #F00, #F00C, #FF0000, #FF0000CC
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		/// For example: #F00, #F00C, #FF0000, #FF0000CC, #FFFF00000000, #FFFF00000000CCCC
#else
#error Not implemented!
#endif
		///</summary>
		///<param name="color">The RGBA hex string or name of the color.</param>
		MagickColor(String^ color);
		///==========================================================================================
		///<summary>
		/// Alpha component value of this color.
		///</summary>
		QUANTUM_CLS_COMPLIANT property Magick::Quantum A;
		///==========================================================================================
		///<summary>
		/// Blue component value of this color.
		///</summary>
		QUANTUM_CLS_COMPLIANT property Magick::Quantum B;
		///==========================================================================================
		///<summary>
		/// Green component value of this color.
		///</summary>
		QUANTUM_CLS_COMPLIANT property Magick::Quantum G;
		///==========================================================================================
		///<summary>
		/// Red component value of this color.
		///</summary>
		QUANTUM_CLS_COMPLIANT property Magick::Quantum R;
		///==========================================================================================
		///<summary>
		/// Returns a transparent color.
		///</summary>
		static property MagickColor^ Transparent
		{
			MagickColor^ get();
		}
		//===========================================================================================
		static bool operator == (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static bool operator != (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static bool operator > (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static bool operator < (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static bool operator >= (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static bool operator <= (MagickColor^ left, MagickColor^ right);
		//===========================================================================================
		static operator Color (MagickColor^ color);
		//===========================================================================================
		static operator MagickColor^ (Color color);
		///==========================================================================================
		///<summary>
		/// Compares the current instance with another object of the same type.
		///</summary>
		///<param name="other">The color to compare this color with.</param>
		virtual int CompareTo(MagickColor^ other);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current color.
		///</summary>
		///<param name="obj">The object to compare this color with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified geometry is equal to the current color.
		///</summary>
		///<param name="other">The color to compare this color with.</param>
		virtual bool Equals(MagickColor^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Converts the value of this instance to an equivalent Color.
		///</summary>
		Color ToColor();
		///==========================================================================================
		///<summary>
		/// Converts the value of this instance to a hexadecimal string.
		///</summary>
		virtual String^ ToString() override;
		//===========================================================================================
	};
	//==============================================================================================
}
