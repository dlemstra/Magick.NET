// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the DrawableCompositeImage object.
    /// </summary>
    public sealed class DrawableComposite : IDrawable, IDrawingWand
    {
        private readonly IMagickImage _image;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="image">The image to draw.</param>
        public DrawableComposite(double x, double y, IMagickImage image)
          : this(image)
        {
            X = x;
            Y = y;
            Width = _image.Width;
            Height = _image.Height;
            Compose = CompositeOperator.CopyAlpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="compose">The algorithm to use.</param>
        /// <param name="image">The image to draw.</param>
        public DrawableComposite(double x, double y, CompositeOperator compose, IMagickImage image)
          : this(image)
        {
            X = x;
            Y = y;
            Width = _image.Width;
            Height = _image.Height;
            Compose = compose;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
        /// </summary>
        /// <param name="offset">The offset from origin.</param>
        /// <param name="image">The image to draw.</param>
        public DrawableComposite(MagickGeometry offset, IMagickImage image)
          : this(image)
        {
            Throw.IfNull(nameof(offset), offset);

            X = offset.X;
            Y = offset.Y;
            Width = offset.Width;
            Height = offset.Height;
            Compose = CompositeOperator.CopyAlpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
        /// </summary>
        /// <param name="offset">The offset from origin.</param>
        /// <param name="compose">The algorithm to use.</param>
        /// <param name="image">The image to draw.</param>
        public DrawableComposite(MagickGeometry offset, CompositeOperator compose, IMagickImage image)
          : this(image)
        {
            Throw.IfNull("offset", offset);

            X = offset.X;
            Y = offset.Y;
            Width = offset.Width;
            Height = offset.Height;
            Compose = compose;
        }

        private DrawableComposite(IMagickImage image)
        {
            Throw.IfNull(nameof(image), image);

            _image = image;
        }

        /// <summary>
        /// Gets or sets the height to scale the image to.
        /// </summary>
        public CompositeOperator Compose { get; set; }

        /// <summary>
        /// Gets or sets the height to scale the image to.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the width to scale the image to.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.Composite(X, Y, Width, Height, Compose, _image);
        }
    }
}