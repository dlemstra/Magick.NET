//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
#include "../Helpers/MagickException.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to access an individual pixel of an image.
	///</summary>
	public ref class Pixel sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		array<Magick::Quantum>^ _Values;
		int _X;
		int _Y;
		//===========================================================================================
	public:
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
		property Magick::Quantum default[int]
		{
			Magick::Quantum get(int channel)
			{
				return GetChannel(channel);
			}
			void set(int channel, Magick::Quantum value)
			{
				SetChannel(channel, value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of channels that the pixel contains.
		///</summary>
		property int Channels
		{
			int get()
			{
				return _Values->Length;
			}
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate of the pixel.
		///</summary>
		property int X
		{
			int get()
			{
				return _X;
			}
			void set(int value)
			{
				if (value < 0)
					return;

				_X = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate of the pixel.
		///</summary>
		property int Y
		{
			int get()
			{
				return _Y;
			}
			void set(int value)
			{
				if (value < 0)
					return;

				_Y = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to get the value of.</param>
		Magick::Quantum GetChannel(int channel);
		///==========================================================================================
		///<summary>
		/// Set the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to set the value of.</param>
		///<param name="value">The value.</param>
		void SetChannel(int channel, Magick::Quantum value);
		//===========================================================================================
	};
	//==============================================================================================
}