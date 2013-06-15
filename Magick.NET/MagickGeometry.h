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
#include "Percentage.h"

using namespace System::Drawing;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick geometry object.
	///</summary>
	public ref class MagickGeometry sealed : MagickWrapper<Magick::Geometry>,
		IEquatable<MagickGeometry^>, IComparable<MagickGeometry^>
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
		static operator const Magick::Geometry& (MagickGeometry^ geometry)
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
		/// The height of the geometry.
		///</summary>
		///==========================================================================================
		property int Height
		{
			int get();
			void set(int value);
		}
		///==========================================================================================
		///<summary>
		/// True if width and height are expressed as percentages.
		///</summary>
		property bool IsPercentage
		{
			bool get();
			void set(bool value);
		}
		///==========================================================================================
		///<summary>
		/// The width of the geometry.
		///</summary>
		property int Width
		{
			int get();
			void set(int value);
		}
		///==========================================================================================
		///<summary>
		/// X offset from origin
		///</summary>
		property int X
		{
			int get();
			void set(int value);
		}
		///==========================================================================================
		///<summary>
		/// Y offset from origin
		///</summary>
		property int Y
		{
			int get();
			void set(int value);
		}
		//===========================================================================================
		static bool operator == (MagickGeometry^ left, MagickGeometry^ right)
		{
			return Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator != (MagickGeometry^ left, MagickGeometry^ right)
		{
			return !Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator > (MagickGeometry^ left, MagickGeometry^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return ReferenceEquals(right, nullptr);

			return left->CompareTo(right) == 1;
		}
		//===========================================================================================
		static bool operator < (MagickGeometry^ left, MagickGeometry^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return !ReferenceEquals(right, nullptr);

			return left->CompareTo(right) == -1;
		}
		//===========================================================================================
		static bool operator >= (MagickGeometry^ left, MagickGeometry^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return ReferenceEquals(right, nullptr);

			return left->CompareTo(right) >= 0;
		}
		//===========================================================================================
		static bool operator <= (MagickGeometry^ left, MagickGeometry^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return !ReferenceEquals(right, nullptr);

			return left->CompareTo(right) <= 0;
		}
		//===========================================================================================
		static explicit operator MagickGeometry^(Rectangle rectangle)
		{
			return gcnew MagickGeometry(rectangle);
		}
		//===========================================================================================
		static explicit operator MagickGeometry^(String^ geometry)
		{
			return gcnew MagickGeometry(geometry);
		}
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
		///<param name="other">The image to compare this geometry with.</param>
		virtual bool Equals(MagickGeometry^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}