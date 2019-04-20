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
    /// destroys the current drawing wand and returns to the  previously pushed drawing wand. Multiple
    /// drawing wands may exist. It is an error to attempt to pop more drawing wands than have been
    /// pushed, and it is proper form to pop all drawing wands which have been pushed.
    /// </summary>
    public sealed class DrawablePopGraphicContext : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePopGraphicContext"/> class.
        /// </summary>
        public DrawablePopGraphicContext()
        {
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.PopGraphicContext();
    }
}