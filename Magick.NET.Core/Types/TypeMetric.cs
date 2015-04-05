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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick TypeMetric object.
	///</summary>
	public sealed class TypeMetric
	{
		///==========================================================================================
		///<summary>
		/// Ascent, the distance in pixels from the text baseline to the highest/upper grid coordinate
		/// used to place an outline point.
		///</summary>
		public double Ascent
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Descent, the distance in pixels from the baseline to the lowest grid coordinate used to
		/// place an outline point. Always a negative value.
		///</summary>
		public double Descent
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Maximum horizontal advance in pixels.
		///</summary>
		public double MaxHorizontalAdvance
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Text height in pixels.
		///</summary>
		public double TextHeight
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Text width in pixels.
		///</summary>
		public double TextWidth
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// Underline position.
		///</summary>
		public double UnderlinePosition
		{
			get;
			set;
		} 
		///==========================================================================================
		///<summary>
		/// Underline thickness.
		///</summary>
		public double UnderlineThickness
		{
			get;
			set;
		} 
		//===========================================================================================
	}
	//==============================================================================================
}