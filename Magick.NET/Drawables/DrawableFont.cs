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

using ImageMagick.Drawables;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableFont object.
	///</summary>
	public sealed class DrawableFont : IDrawableFont
	{
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableFont instance.
		///</summary>
		///<param name="family">The font family or the full path to the font file.</param>
		public DrawableFont(string family)
			: this(family, FontStyleType.Any, FontWeight.Weight400, FontStretch.Normal)
		{
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableFont instance.
		///</summary>
		///<param name="family">The font family or the full path to the font file.</param>
		///<param name="style">The style of the font.</param>
		///<param name="weight">The weight of the font.</param>
		///<param name="stretch">The font stretching type.</param>
		public DrawableFont(string family, FontStyleType style, FontWeight weight, FontStretch stretch)
		{
			Family = family;
			Style = style;
			Weight = weight;
			Stretch = stretch;
		}
		///==========================================================================================
		///<summary>
		/// The font family or the full path to the font file.
		///</summary>
		public string Family
		{
			get;
			set;
		}
		///==========================================================================================
		/// <summary>
		/// The style of the font,
		/// </summary>
		public FontStyleType Style
		{
			get;
			set;
		}
		///==========================================================================================
		/// <summary>
		/// The weight of the font,
		/// </summary>
		public FontWeight Weight
		{
			get;
			set;
		}
		///==========================================================================================
		/// <summary>
		/// FontStretch
		/// </summary>
		public FontStretch Stretch
		{
			get;
			set;
		}
		//===========================================================================================
	}
	//==============================================================================================
}