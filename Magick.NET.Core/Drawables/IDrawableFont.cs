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

namespace ImageMagick.Drawables
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableFont object.
	///</summary>
	public interface IDrawableFont : IDrawable
	{
		///==========================================================================================
		///<summary>
		/// The font family or the full path to the font file.
		///</summary>
		string Family
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// The style of the font,
		/// </summary>
		FontStyleType Style
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// The weight of the font,
		/// </summary>
		FontWeight Weight
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// FontStretch
		/// </summary>
		FontStretch Stretch
		{
			get;
		}
		//===========================================================================================
	}
	//==============================================================================================
}