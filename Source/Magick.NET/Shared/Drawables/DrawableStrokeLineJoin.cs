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
    /// Specifies the shape to be used at the corners of paths (or other vector shapes) when they
    /// are stroked.
    /// </summary>
    public sealed class DrawableStrokeLineJoin : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableStrokeLineJoin"/> class.
        /// </summary>
        /// <param name="lineJoin">The line join.</param>
        public DrawableStrokeLineJoin(LineJoin lineJoin)
        {
            LineJoin = lineJoin;
        }

        /// <summary>
        /// Gets or sets the line join.
        /// </summary>
        public LineJoin LineJoin
        {
            get;
            set;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.StrokeLineJoin(LineJoin);
        }
    }
}