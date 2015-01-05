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
#pragma once
#include "..\Exceptions\Base\MagickException.h"
#include "..\Colors\MagickColor.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to access an individual pixel of an image.
	///</summary>
	public ref class Pixel sealed : IEquatable<Pixel^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		array<Magick::Quantum>^ _Value;
		int _X;
		int _Y;
		//===========================================================================================
		Pixel();
		//===========================================================================================
		static void CheckChannels(int channels);
		//===========================================================================================
		void Pixel::Initialize(int x, int y, array<Magick::Quantum>^ value);
		//===========================================================================================
	internal:
		//===========================================================================================
		property array<Magick::Quantum>^ Value
		{
			array<Magick::Quantum>^ get();
		}
		//===========================================================================================
		static Pixel^ Create(int x, int y, array<Magick::Quantum>^ value);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new Pixel instance.
		///</summary>
		///<param name="x">The X coordinate of the pixel.</param>
		///<param name="y">The Y coordinate of the pixel.</param>
		///<param name="value">The value of the pixel.</param>
		QUANTUM_CLS_COMPLIANT Pixel(int x, int y, array<Magick::Quantum>^ value);
		///==========================================================================================
		///<summary>
		/// Creates a new Pixel instance.
		///</summary>
		///<param name="x">The X coordinate of the pixel.</param>
		///<param name="y">The Y coordinate of the pixel.</param>
		///<param name="channels">The number of channels.</param>
		Pixel(int x, int y, int channels);
		///==========================================================================================
		///<summary>
		/// Returns the value of the specified channel.
		///</summary>
		QUANTUM_CLS_COMPLIANT property Magick::Quantum default[int]
		{
			Magick::Quantum get(int channel);
			void set(int channel, Magick::Quantum value);
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of channels that the pixel contains.
		///</summary>
		property int Channels
		{
			int get();
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate of the pixel.
		///</summary>
		property int X
		{
			int get();
			void set(int value);
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate of the pixel.
		///</summary>
		property int Y
		{
			int get();
			void set(int value);
		}
		//===========================================================================================
		static bool operator == (Pixel^ left, Pixel^ right);
		//===========================================================================================
		static bool operator != (Pixel^ left, Pixel^ right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current pixel.
		///</summary>
		///<param name="obj">The object to compare this color with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified pixel is equal to the current pixel.
		///</summary>
		///<param name="other">The pixel to compare this color with.</param>
		virtual bool Equals(Pixel^ other);
		///==========================================================================================
		///<summary>
		/// Returns the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to get the value of.</param>
		QUANTUM_CLS_COMPLIANT Magick::Quantum GetChannel(int channel);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Set the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to set the value of.</param>
		///<param name="value">The value.</param>
		QUANTUM_CLS_COMPLIANT void SetChannel(int channel, Magick::Quantum value);
		///==========================================================================================
		///<summary>
		/// Converts the pixel to a color. Assumes the pixel is RGBA.
		///</summary>
		MagickColor^ ToColor();
		//===========================================================================================
	};
	//==============================================================================================
}