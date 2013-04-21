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

#include "Base\MagickWrapper.h"
#include "Helpers\Percentage.h"

using namespace System::Drawing;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick geometry object.
	///</summary>
	public ref class MagickGeometry sealed : MagickWrapper<Magick::Geometry>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(int x, int y, int width, int height, bool isPercentage);
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickGeometry(Magick::Geometry geometry);
		//===========================================================================================
		static operator Magick::Geometry& (MagickGeometry^ geometry)
		{
			return *(geometry->Value);
		}
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified width and
		/// height.
		///</summary>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		MagickGeometry(int width, int height);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified width and
		/// height.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the  height.</param>
		MagickGeometry(Percentage percentageWidth, Percentage percentageHeight);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified offsets, width
		/// and height.
		///</summary>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		//===========================================================================================
		MagickGeometry(int x, int y, int width, int height);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified offsets, width
		/// and height.
		///</summary>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the  height.</param>
		//===========================================================================================
		MagickGeometry(int x, int y, Percentage percentageWidth, Percentage percentageHeight);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified rectangle.
		///</summary>
		MagickGeometry(Rectangle rectangle);
		///==========================================================================================
		///<summary>
		/// The height of the geometry.
		///</summary>
		///==========================================================================================
		property int Height
		{
			int get()
			{
				return Value->height();
			}
			void set(int value)
			{
				Value->height(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// True if width and height are expressed as percentages.
		///</summary>
		property bool IsPercentage
		{
			bool get()
			{
				return Value->percent();
			}
			void set(bool value)
			{
				Value->percent(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The width of the geometry.
		///</summary>
		property int Width
		{
			int get()
			{
				return Value->width();
			}
			void set(int value)
			{
				Value->width(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// X offset from origin
		///</summary>
		property int X
		{
			int get()
			{
				return Value->xNegative() ? -1 * Value->xOff() : Value->xOff();
			}
			void set(int value)
			{
				Value->xOff(value);
				Value->xNegative(value < 0);
			}
		}
		///==========================================================================================
		///<summary>
		/// Y offset from origin
		///</summary>
		property int Y
		{
			int get()
			{
				return Value->yNegative() ? -1 * Value->yOff() : Value->yOff();
			}
			void set(int value)
			{
				Value->yOff(value);
				Value->yNegative(value < 0);
			}
		}
		//===========================================================================================
		static explicit operator MagickGeometry^(Rectangle rectangle)
		{
			return FromRectangle(rectangle);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified Rectangle to an instane of this type.
		///</summary>
		static MagickGeometry^ FromRectangle(Rectangle rectangle);
	};
	//==============================================================================================
}