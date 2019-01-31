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
    /// Specifies the miter limit. When two line segments meet at a sharp angle and miter joins have
    /// been specified for 'DrawableStrokeLineJoin', it is possible for the miter to extend far
    /// beyond the thickness of the line stroking the path. The 'DrawableStrokeMiterLimit' imposes a
    /// limit on the ratio of the miter length to the 'DrawableStrokeLineWidth'.
    /// </summary>
    public sealed class DrawableStrokeMiterLimit : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableStrokeMiterLimit"/> class.
        /// </summary>
        /// <param name="miterlimit">The miter limit.</param>
        public DrawableStrokeMiterLimit(int miterlimit)
        {
            Miterlimit = miterlimit;
        }

        /// <summary>
        /// Gets or sets the miter limit.
        /// </summary>
        public int Miterlimit
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
                wand.StrokeMiterLimit(Miterlimit);
        }
    }
}