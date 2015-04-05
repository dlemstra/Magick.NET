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

using System.Drawing.Drawing2D;
using ImageMagick.Drawables;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates a 3-by-3 affine matrix that represents a geometric transform.
	///</summary>
	public sealed class DrawableAffine : IDrawableAffine
	{
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableAffine instance.
		///</summary>
		///<param name="scaleX">The X coordinate scaling element.</param>
		///<param name="scaleY">The Y coordinate scaling element.</param>
		///<param name="shearX">The X coordinate shearing element.</param>
		///<param name="shearY">The Y coordinate shearing element.</param>
		///<param name="translateX">The X coordinate of the translation element.</param>
		///<param name="translateY">The Y coordinate of the translation element.</param>
		public DrawableAffine(double scaleX, double scaleY, double shearX, double shearY, double translateX,
			double translateY)
		{
			ScaleX = scaleX;
			ScaleY = scaleY;
			ShearX = shearX;
			ShearY = shearY;
			TranslateX = translateX;
			TranslateY = translateY;
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableAffine instance using the specified Matrix.
		///</summary>
		///<param name="matrix">The matrix.</param>
		public DrawableAffine(Matrix matrix)
		{
			Throw.IfNull("matrix", matrix);

			ScaleX = (double)matrix.Elements[0];
			ScaleY = (double)matrix.Elements[1];
			ShearX = (double)matrix.Elements[2];
			ShearY = (double)matrix.Elements[3];
			TranslateX = (double)matrix.Elements[4];
			TranslateY = (double)matrix.Elements[5];
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate scaling element.
		///</summary>
		public double ScaleX
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate scaling element.
		///</summary>
		public double ScaleY
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate shearing element.
		///</summary>
		public double ShearX
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate shearing element.
		///</summary>
		public double ShearY
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate of the translation element.
		///</summary>
		public double TranslateX
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate of the translation element.
		///</summary>
		public double TranslateY
		{
			get;
			set;
		}
		//===========================================================================================
	}
	//==============================================================================================
}