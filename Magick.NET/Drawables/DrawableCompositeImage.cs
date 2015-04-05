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
	/// Encapsulation of the DrawableCompositeImage object.
	///</summary>
	public sealed class DrawableCompositeImage : IDrawableCompositeImage
	{
		//===========================================================================================
		private MagickImage _Image;
		//===========================================================================================
		private DrawableCompositeImage([ValidatedNotNull] MagickImage image)
		{
			Throw.IfNull("image", image);

			_Image = image;
		}
		//===========================================================================================
		Internal.IMagickImage IDrawableCompositeImage.Image
		{
			get
			{
				return MagickImage.GetInstance(_Image);
			}
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="image">The image to draw.</param>
		public DrawableCompositeImage(double x, double y, MagickImage image)
			: this(image)
		{
			X = x;
			Y = y;
			Width = image.Width;
			Height = image.Height;
			Compose = CompositeOperator.CopyAlpha;
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="image">The image to draw.</param>
		public DrawableCompositeImage(double x, double y, CompositeOperator compose, MagickImage image)
			: this(image)
		{
			X = x;
			Y = y;
			Width = image.Width;
			Height = image.Height;
			Compose = compose;
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="offset">The offset from origin.</param>
		///<param name="image">The image to draw.</param>
		public DrawableCompositeImage(MagickGeometry offset, MagickImage image)
			: this(image)
		{
			Throw.IfNull("offset", offset);

			X = offset.X;
			Y = offset.Y;
			Width = offset.Width;
			Height = offset.Height;
			Compose = CompositeOperator.CopyAlpha;
		}
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="offset">The offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="image">The image to draw.</param>
		public DrawableCompositeImage(MagickGeometry offset, CompositeOperator compose, MagickImage image)
			: this(image)
		{
			Throw.IfNull("offset", offset);

			X = offset.X;
			Y = offset.Y;
			Width = offset.Width;
			Height = offset.Height;
			Compose = compose;
		}
		///==========================================================================================
		///<summary>
		/// The height to scale the image to.
		///</summary>
		public CompositeOperator Compose
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The height to scale the image to.
		///</summary>
		public double Height
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The width to scale the image to.
		///</summary>
		public double Width
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate.
		///</summary>
		public double X
		{
			get;
			set;
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate.
		///</summary>
		public double Y
		{
			get;
			set;
		}
		//===========================================================================================
	}
	//==============================================================================================
}