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

#include "..\Base\MagickWrapper.h"

using namespace System::Drawing;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick geometry object.
	///</summary>
	public ref class MagickGeometry sealed : IEquatable<MagickGeometry^>, IComparable<MagickGeometry^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(Magick::Geometry geometry);
		//===========================================================================================
		void Initialize(int x, int y, int width, int height, bool isPercentage);
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickGeometry(Magick::Geometry geometry);
		//===========================================================================================
		const Magick::Geometry* CreateGeometry();
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified width and
		/// height.
		///</summary>
		///<param name="widthAndHeight">The width and height.</param>
		MagickGeometry(int widthAndHeight);
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
		///<param name="rectangle">The rectangle to use.</param>
		MagickGeometry(Rectangle rectangle);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickGeometry class using the specified geometry.
		///</summary>
		///<param name="value">Geometry specifications in the form: &lt;width&gt;x&lt;height&gt;
		///{+-}&lt;xoffset&gt;{+-}&lt;yoffset&gt; (where width, height, xoffset, and yoffset are numbers)</param>
		MagickGeometry(String^ value);
		///==========================================================================================
		///<summary>
		/// Resize the image based on the smallest fitting dimension (^).
		///</summary>
		property bool FillArea;
		///==========================================================================================
		///<summary>
		/// Resize if image is greater than size (&gt;)
		///</summary>
		property bool Greater;
		///==========================================================================================
		///<summary>
		/// The height of the geometry.
		///</summary>
		///==========================================================================================
		property int Height;
		///==========================================================================================
		///<summary>
		/// Resize without preserving aspect ratio (!)
		///</summary>
		property bool IgnoreAspectRatio;
		///==========================================================================================
		///<summary>
		/// True if width and height are expressed as percentages.
		///</summary>
		property bool IsPercentage;
		///==========================================================================================
		///<summary>
		/// Resize if image is less than size (&lt;)
		///</summary>
		property bool Less;
		///==========================================================================================
		///<summary>
		/// Resize using a pixel area count limit (@).
		///</summary>
		property bool LimitPixels;
		///==========================================================================================
		///<summary>
		/// The width of the geometry.
		///</summary>
		property int Width;
		///==========================================================================================
		///<summary>
		/// X offset from origin
		///</summary>
		property int X;
		///==========================================================================================
		///<summary>
		/// Y offset from origin
		///</summary>
		property int Y;
		//===========================================================================================
		static bool operator == (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static bool operator != (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static bool operator > (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static bool operator < (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static bool operator >= (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static bool operator <= (MagickGeometry^ left, MagickGeometry^ right);
		//===========================================================================================
		static explicit operator MagickGeometry^(Rectangle rectangle);
		//===========================================================================================
		static explicit operator MagickGeometry^(String^ geometry);
		///==========================================================================================
		///<summary>
		/// Compares the current instance with another object of the same type.
		///</summary>
		///<param name="other">The object to compare this geometry with.</param>
		virtual int CompareTo(MagickGeometry^ other);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current geometry.
		///</summary>
		///<param name="obj">The object to compare this geometry with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified geometry is equal to the current geometry.
		///</summary>
		///<param name="other">The geometry to compare this geometry with.</param>
		virtual bool Equals(MagickGeometry^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current geometry.
		///</summary>
		virtual String^ ToString() override;
		//===========================================================================================
	};
	//==============================================================================================
}